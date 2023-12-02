using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using Newtonsoft.Json;
using System;

namespace CommandHotkeys
{
    public class CommandHotkeys : BaseScript
    {
        private Configuration Configuration = JsonConvert.DeserializeObject<Configuration>(LoadResourceFile(GetCurrentResourceName(), "config.json"));
        public CommandHotkeys() {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private async void OnClientResourceStart(string obj)
        {
            if (GetCurrentResourceName() != obj) return;
            while (true)
            {
                foreach (var command in Configuration.Commands)
                {
                    if (IsControlJustPressed(0, command.Value)) {
                        ExecuteCommand(command.Key);
                    }
                }
                await Delay(5);
            }
        }
    }
}
