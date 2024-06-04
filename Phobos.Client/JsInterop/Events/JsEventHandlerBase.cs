using Microsoft.JSInterop;

namespace Phobos.Client.JsInterop.Events;

public abstract class JsEventHandlerBase<TEvent> : IJsEventHandler<TEvent>
{
    private readonly List<Func<TEvent, Task>> subscribers = new();
    
    [JSInvokable]
    public virtual async Task HandleAsync(TEvent @event)
    {
        var tasks = new List<Task>();
        
        foreach (var subscriber in subscribers)
        {
            tasks.Add(subscriber.Invoke(@event));
        }

        await Task.WhenAll(tasks);
    }

    public virtual void Subscribe(Func<TEvent, Task> callback)
    {
        subscribers.Add(callback);
    }
}