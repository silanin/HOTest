using HiddenObject.Commands;
using HiddenObject.Events;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using HiddenObject.Models;
using HiddenObject.UI.Windows;
using UnityEngine;

public class GameContext : MVCSContext
{
    public GameContext(MonoBehaviour view, bool autoStartup) : base(view, autoStartup) { }

    public override void Launch()
    {
        base.Launch();
        var startSignal = injectionBinder.GetInstance<StartGameSignal>();
        startSignal.Dispatch();
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
        injectionBinder.Bind<IGameModel>().To<GameModel>().ToSingleton();

        // signals without commands
        injectionBinder.Bind<ShowGameSignal>().ToSingleton();
        injectionBinder.Bind<HideGameSignal>().ToSingleton();
        injectionBinder.Bind<ShowLevelCompleteSignal>().ToSingleton();
        injectionBinder.Bind<HideLevelCompleteSignal>().ToSingleton();

        // views
        mediationBinder.Bind<GameView>().To<GameMediator>();
        mediationBinder.Bind<LevelCompletePopupView>().To<LevelCompletePopupMediator>();

        // signals to commands
        commandBinder.Bind<StartGameSignal>().To<StartGameCommand>();
        commandBinder.Bind<SaveProgressSignal>().To<SaveProgressCommand>();
        commandBinder.Bind<BackToMenuSignal>().To<BackToMenuCommand>();
        commandBinder.Bind<ReplaySignal>().To<ReplayCommand>();
        commandBinder.Bind<CollectObjectSignal>().To<CollectObjectCommand>();
        commandBinder.Bind<NightModeSignal>().To<NightModeCommand>();
    }
}
