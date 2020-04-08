using strange.extensions.context.impl;

public class GameContextView : ContextView
{
    void Awake()
    {
        context = new GameContext(this, true);
        context.Start();
    }
}