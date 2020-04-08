using strange.extensions.context.impl;

public class RootContextView : ContextView
{
    void Awake()
    {
        context = new RootContext(this, true);
        context.Start();
    }
}