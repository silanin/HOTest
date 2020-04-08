using strange.extensions.command.impl;
using HiddenObject.Events;

namespace HiddenObject.Commands
{
    public class ShowMenuCommand : Command
    {
        [Inject]
        public ShowMenuSignal showMenuSignal { get; set; }

        public override void Execute()
        {
            showMenuSignal.Dispatch();
        }
    }
}

