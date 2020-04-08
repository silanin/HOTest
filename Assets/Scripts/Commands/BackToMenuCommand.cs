using System;
using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using HiddenObject.Enums;
using HiddenObject.Models;
using HiddenObject.Events;
using HiddenObject.UI.Configuration;

namespace HiddenObject.Commands
{
    public class BackToMenuCommand : Command
    {
        [Inject]
        public IGameModel gameModel { get; set; }

        [Inject]
        public HideGameSignal hideGameSignal { get; set; }

        [Inject]
        public ShowMenuSignal showMenuSignal { get; set; }

        public override void Execute()
        {
            Retain();

            gameModel.Reset();

            hideGameSignal.Dispatch();

            // TODO: add preloader

            UnloadScene(SceneType.Game).Subscribe(onNext =>
            {
                showMenuSignal.Dispatch();
                Release();
            });
        }

        private IObservable<AsyncOperation> UnloadScene(SceneType type)
        {
            var uiConfiguration = Resources.Load<UIConfiguration>("UIConfiguration");
            var scene = uiConfiguration.GetScene(type);
            return SceneManager.UnloadSceneAsync(scene.Scene).AsAsyncOperationObservable();
        }
    }
}

