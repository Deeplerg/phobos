using Phobos.Core.Drawing.Actions;

namespace Phobos.Core.Drawing.Pipeline;

public interface IDrawingPipeline<TDrawingOptions>
{
    void Draw(TDrawingOptions options);

    void AddAction<TDrawingAction>()
        where TDrawingAction : IDrawingAction<TDrawingOptions>;
    void ClearActions();
}