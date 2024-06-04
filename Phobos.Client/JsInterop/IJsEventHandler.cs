namespace Phobos.Client.JsInterop;

public interface IJsEventHandler<TEvent>
{
    Task HandleAsync(TEvent @event);
    void Subscribe(Func<TEvent, Task> callback);
}