using System;
using HiddenObject.Api.Responses;
using HiddenObject.Enums;
using HiddenObject.Models;
using HiddenObject.Services;
using strange.extensions.command.impl;
using UniRx;
using UnityEngine;

namespace HiddenObject.Commands
{
    public class GetUserProgressCommand : Command
    {
        [Inject]
        public IStorageService storageService { get; set; }

        [Inject]
        public INetConnection connection { get; set; }

        [Inject]
        public IUserProgressModel progressModel { get; set; }

        public override void Execute()
        {
            var save = storageService.LoadModel<IUserProgressModel>(ModelName.Progress);
            if (save != null)
            {
                progressModel.Update(save.Progress);
            }
            else
            {
                Retain();

                connection.GetUserProgress().Subscribe(OnSuccess, OnError);
            }
        }

        private void OnSuccess(UserProgressResponse response)
        {
            progressModel.Update(response.Progress);

            storageService.SaveModel(ModelName.Progress, progressModel);

            Release();
        }

        private void OnError(Exception exception)
        {
            Release();
        }
    }
}

