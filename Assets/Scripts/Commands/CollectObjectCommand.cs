using System;
using System.Linq;
using strange.extensions.command.impl;
using UniRx;
using HiddenObject.Api.Responses;
using HiddenObject.Constants;
using HiddenObject.Models;
using HiddenObject.Services;

namespace HiddenObject.Commands
{
    public class CollectObjectCommand : Command
    {
        [Inject]
        public int objectId { get; set; }

        [Inject]
        public INetConnection connection { get; set; }

        [Inject]
        public IGameModel gameModel { get; set; }

        public override void Execute()
        {
            connection.CollectObject(gameModel.Level, objectId).Subscribe(OnSuccess, OnError);
        }

        private void OnSuccess(BoolResponse response)
        {
            if (response.Result)
            {
                var current = gameModel.Objects.FirstOrDefault(x => x.Id == objectId);
                if (current != null && !current.Collected)
                {
                    var collected = gameModel.ObjectsCollected.Value + 1;

                    var stars = (int)Math.Floor((double)collected / (gameModel.Objects.Count / GameConstants.StarsNumber));
                    gameModel.Stars.Value = stars;

                    current.Collected = true;
                    gameModel.ObjectsCollected.Value = collected;
                }
            }

            Release();
        }

        private void OnError(Exception exception)
        {
            Release();
        }
    }
}

