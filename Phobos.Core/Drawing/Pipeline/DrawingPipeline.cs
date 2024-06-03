using Microsoft.Extensions.DependencyInjection;
using Phobos.Core.Drawing.Actions;

namespace Phobos.Core.Drawing.Pipeline;

public class DrawingPipeline : IDrawingPipeline<DrawingOptions>
{
    private readonly IServiceProvider _provider;
    private readonly List<Type> _actions = new();

    public DrawingPipeline(IServiceProvider provider)
    {
        _provider = provider;
    }
    
    public void Draw(DrawingOptions options)
    {
        if (!_actions.Any())
        {
            return;
        }
        
        DrawActionDelegate<DrawingOptions>? next = PipelineEnd;
        IDrawingAction<DrawingOptions>? lastInstance = null;
        for (int i = _actions.Count - 1; i >= 0; i--)
        {
            var @delegate = (DrawActionDelegate<DrawingOptions>?)next?.Clone();
            
            var currentActionType = _actions[i];
            var currentAction = CreateActionInstance(currentActionType, @delegate);

            next = currentAction.Draw;
            lastInstance = currentAction;
        }
        
        lastInstance!.Draw(options);
    }

    public void AddAction<TDrawingAction>() where TDrawingAction : IDrawingAction<DrawingOptions>
    {
        _actions.Add(typeof(TDrawingAction));
    }

    public void ClearActions()
    {
        _actions.Clear();
    }

    private IDrawingAction<DrawingOptions> CreateActionInstance(
        Type actionType,
        DrawActionDelegate<DrawingOptions>? next)
    {
        return (IDrawingAction<DrawingOptions>)ActivatorUtilities.CreateInstance(_provider, actionType, next!);
    }
    
    // this is needed because if next is null, then ActivatorUtilities thinks that we didn't provide any parameters
    // and therefore it doesn't find the constructor with the delegate
    private void PipelineEnd(DrawingOptions options) { }
}