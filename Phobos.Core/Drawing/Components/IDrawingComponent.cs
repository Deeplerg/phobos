namespace Phobos.Core.Drawing.Components;

public interface IDrawingComponent<TDrawingOptions>
{
    void Draw(TDrawingOptions options);
}