using System;
using HiddenObject.Commands;
using HiddenObject.Events;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using HiddenObject.Services;
using HiddenObject.Models;
using HiddenObject.UI.Windows;

public class RootContext : MVCSContext
{
    private IDisposable _subscription;

    public RootContext(MonoBehaviour view, bool autoStartup) : base(view, autoStartup)
    {
        _subscription = view.OnDestroyAsObservable().Subscribe(DestroyContext);
    }

    public override void Launch()
    {
        base.Launch();
        var startSignal = injectionBinder.GetInstance<StartSignal>();
        startSignal.Dispatch();
    }

    public void DestroyContext(Unit value)
    {
        _subscription?.Dispose();

        var destroySignal = injectionBinder.GetInstance<DestroySignal>();
        destroySignal.Dispatch();
    }

    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }

    protected override void mapBindings()
    {
        base.mapBindings();

        // bind models
        injectionBinder.Bind<ILevelsModel>().To<LevelsModel>().ToSingleton().CrossContext();
        injectionBinder.Bind<IUserProgressModel>().To<UserProgressModel>().ToSingleton().CrossContext();
        injectionBinder.Bind<ISettingsModel>().To<SettingsModel>().ToSingleton().CrossContext();

        // bind services
        injectionBinder.Bind<IStorageService>().To<StorageService>().ToSingleton().CrossContext();
        injectionBinder.Bind<INetConnection>().To<NetConnection>().ToSingleton().CrossContext();

        // signals without commands
        injectionBinder.Bind<ShowMenuSignal>().ToSingleton().CrossContext();
        injectionBinder.Bind<HideMenuSignal>().ToSingleton().CrossContext();

        // bind views
        mediationBinder.Bind<GameMenuView>().To<GameMenuMediator>();

        // bind signals to commands
        commandBinder.Bind<StartSignal>().InSequence()
            .To<InitializeServicesCommand>()
            .To<GetLevelsCommand>()
            .To<GetUserProgressCommand>()
            .To<ShowMenuCommand>();

        commandBinder.Bind<DestroySignal>().To<DestroyServicesCommand>();

        commandBinder.Bind<LoadLevelSignal>().To<LoadLevelCommand>();
        commandBinder.Bind<NightModeSignal>().To<NightModeCommand>();
    }
}
