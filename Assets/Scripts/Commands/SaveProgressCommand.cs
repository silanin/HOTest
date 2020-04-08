using System;
using strange.extensions.command.impl;
using UniRx;
using HiddenObject.Api.Dto;
using HiddenObject.Api.Requests;
using HiddenObject.Api.Responses;
using HiddenObject.Enums;
using HiddenObject.Models;
using HiddenObject.Services;

namespace HiddenObject.Commands
{
    public class SaveProgressCommand : Command
    {
        [Inject]
        public IUserProgressModel progress { get; set; }

        [Inject]
        public IGameModel gameModel { get; set; }

        [Inject]
        public INetConnection connection { get; set; }

        [Inject]
        public IStorageService storageService { get; set; }


        public override void Execute()
        {
            Retain();

            var levelProgress = new LevelProgressDto
            {
                Level = gameModel.Level,
                Stars = gameModel.Stars.Value,
                ObjectsCollected = gameModel.ObjectsCollected.Value,
                Seconds = gameModel.Seconds.Value
            };


            var current = progress.Get(gameModel.Level);
            if (current != null)
            {
                if (current.Stars < levelProgress.Stars)
                {
                    current.Stars = levelProgress.Stars;
                    current.Seconds = levelProgress.Seconds;
                    current.ObjectsCollected = levelProgress.ObjectsCollected;
                }
            }
            else
            {
                Retain();

                progress.Progress.Add(levelProgress);

                var contract = new LevelProgressRequest
                {
                    LevelProgress = levelProgress
                };

                connection.SetProgress(gameModel.Level, contract).Subscribe(OnSuccess, OnError);
            }
        }

        private void OnSuccess(BoolResponse response)
        {
            if (response.Result)
            {
                storageService.SaveModel(ModelName.Progress, progress);

                Release();
            }
        }

        private void OnError(Exception exception)
        {
            Release();
        }
    }
}



