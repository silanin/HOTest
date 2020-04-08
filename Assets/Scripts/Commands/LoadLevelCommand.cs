using System;
using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using HiddenObject.Enums;
using HiddenObject.UI.Configuration;
using HiddenObject.Events;

namespace HiddenObject.Commands
{
    public class LoadLevelCommand : Command
    {
        [Inject]
        public int level { get; set; }

        [Inject]
        public HideMenuSignal hideMenuSignal { get; set; }

        public override void Execute()
        {
            Retain();

            hideMenuSignal.Dispatch();

            // TODO: add preloader

            LoadScene(SceneType.Game).Subscribe(onNext =>
            {
                Release();
            });
        }

        private IObservable<AsyncOperation> LoadScene(SceneType type)
        {
            var uiConfiguration = Resources.Load<UIConfiguration>("UIConfiguration");
            var scene = uiConfiguration.GetScene(type);
            return SceneManager.LoadSceneAsync(scene.Scene, scene.LoadType).AsAsyncOperationObservable();
        }
    }
}

