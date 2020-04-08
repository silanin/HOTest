using strange.extensions.command.impl;
using HiddenObject.Models;

namespace HiddenObject.Commands
{
    public class NightModeCommand : Command
    {
        [Inject]
        public bool nightMode { get; set; }

        [Inject]
        public ISettingsModel settingsModel { get; set; }

        public override void Execute()
        {
            settingsModel.NightMode.Value = nightMode;
        }
    }
}

