using strange.extensions.command.impl;
using HiddenObject.Services;

namespace HiddenObject.Commands
{
    public class DestroyServicesCommand : Command
    {
        [Inject]
        public INetConnection connection { get; set; }

        [Inject]
        public IStorageService storageService { get; set; }

        public override void Execute()
        {
            connection.Dispose();
            storageService.Dispose();
        }
    }
}