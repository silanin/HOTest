using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

public interface IService
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    IEventDispatcher dispatcher { get; set; }

    bool Initialized { get; }

    void Init();
}
