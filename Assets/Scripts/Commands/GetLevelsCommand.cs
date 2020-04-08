using System;
using HiddenObject.Api.Responses;
using HiddenObject.Enums;
using HiddenObject.Models;
using HiddenObject.Services;
using strange.extensions.command.impl;
using UniRx;

namespace HiddenObject.Commands
{
    public class GetLevelsCommand : Command
    {
        [Inject]
        public IStorageService storageService { get; set; }

        [Inject]
        public INetConnection connection { get; set; }

        [Inject]
        public ILevelsModel levelsModel { get; set; }

        public override void Execute()
        {
            var save = storageService.LoadModel<ILevelsModel>(ModelName.Levels);
            if (save != null)
            {
                levelsModel.Update(save.Levels);
            }
            else
            {
                Retain();

                connection.GetLevels().Subscribe(OnSuccess, OnError);
            }
        }

        private void OnSuccess(LevelsResponse response)
        {
            levelsModel.Update(response.Levels);

            storageService.SaveModel(ModelName.Levels, levelsModel);

            Release();
        }

        private void OnError(Exception exception)
        {
            Release();
        }
    }
}

