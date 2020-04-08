using strange.extensions.signal.impl;

namespace HiddenObject.Events
{
    public class StartSignal : Signal { }

    public class DestroySignal : Signal { }

    public class StartGameSignal : Signal { }

    public class LoadLevelSignal : Signal<int> { }

    public class NightModeSignal : Signal<bool> { }

    public class ReplaySignal : Signal { }

    public class ShowMenuSignal : Signal { }

    public class HideMenuSignal : Signal { }

    public class BackToMenuSignal : Signal { }

    public class ShowGameSignal : Signal { }

    public class HideGameSignal : Signal { }

    public class LevelStartedSignal : Signal { }

    public class LevelCompletedSignal : Signal { }

    public class CollectObjectSignal : Signal<int> { }

    public class SaveProgressSignal : Signal { }

    public class ShowLevelCompleteSignal : Signal { }

    public class HideLevelCompleteSignal : Signal { }

}

