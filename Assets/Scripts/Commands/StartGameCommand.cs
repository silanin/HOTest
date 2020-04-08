using System;
using System.Linq;
using HiddenObject.Constants;
using HiddenObject.Events;
using HiddenObject.Models;
using strange.extensions.command.impl;
using UniRx;
using UnityEngine;

namespace HiddenObject.Commands
{
    public class StartGameCommand : Command
    {
        [Inject]
        public ILevelsModel levels { get; set; }

        [Inject]
        public IUserProgressModel progress { get; set; }

        [Inject]
        public IGameModel gameModel { get; set; }

        [Inject]
        public ShowGameSignal showGameSignal { get; set; }

        [Inject]
        public ShowLevelCompleteSignal showLevelCompleteSignal { get; set; }

        [Inject]
        public SaveProgressSignal saveProgressSignal { get; set; }

        private IDisposable _timerSubscription;
        private IDisposable _collectedSubscription;


        public override void Execute()
        {
            Retain();

            var level = progress.Progress.Count > 0 ? progress.Progress.Max(x => x.Level) : GameConstants.StartLevel;
            var levelData = levels.Get(level);

            gameModel.Update(levelData);

            showGameSignal.Dispatch();

            _timerSubscription = Observable.Interval(TimeSpan.FromSeconds(1f)).Subscribe(OnTimerTick);

            _collectedSubscription = gameModel.ObjectsCollected.Subscribe(OnObjectsCollectedChange);
        }

        private void OnObjectsCollectedChange(int value)
        {
            if (value > 0 && gameModel.ObjectsCollected.Value >= gameModel.Objects.Count)
            {
                GameComplete();
            }
        }

        private void OnTimerTick(long value)
        {
            if (gameModel.Completed.Value)
            {
                _timerSubscription.Dispose();
            }
            else
            {
                gameModel.Seconds.Value++;

                if (gameModel.Seconds.Value >= gameModel.TotalSeconds)
                {
                    GameComplete();
                }
            }
        }

        private void GameComplete()
        {
            gameModel.Completed.Value = true;

            showLevelCompleteSignal.Dispatch();

            _timerSubscription.Dispose();
            _collectedSubscription.Dispose();

            saveProgressSignal.Dispatch();

            Release();
        }
    }
}


