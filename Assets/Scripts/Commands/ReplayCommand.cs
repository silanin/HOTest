using strange.extensions.command.impl;
using HiddenObject.Events;

namespace HiddenObject.Commands
{
    public class ReplayCommand : Command
    {
        [Inject]
        public StartGameSignal startGameSignal { get; set; }

        public override void Execute()
        {
            startGameSignal.Dispatch();
        }
    }
}