namespace Phobos.Core.Drawing.Actions;

public interface IDrawingAction<TDrawingOptions>
{
    void Draw(TDrawingOptions options);
}