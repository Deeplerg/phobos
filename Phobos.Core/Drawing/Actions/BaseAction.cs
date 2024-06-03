using Phobos.Core.Drawing.Components;

namespace Phobos.Core.Drawing.Actions;

public class BaseAction : IDrawingAction<DrawingOptions>
{
    private readonly DrawActionDelegate<DrawingOptions>? _next;
    private readonly IDrawingComponent<DrawingOptions> _component;

    public BaseAction(DrawActionDelegate<DrawingOptions>? next, IDrawingComponent<DrawingOptions> component)
    {
        _next = next;
        _component = component;
    }
    
    public void Draw(DrawingOptions options)
    {
        _component.Draw(options);
        
        _next?.Invoke(options);
    }
}