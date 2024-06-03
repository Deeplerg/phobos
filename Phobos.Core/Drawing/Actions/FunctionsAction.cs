using Phobos.Core.Drawing.Components;

namespace Phobos.Core.Drawing.Actions;

public class FunctionsAction : BaseAction
{
    public FunctionsAction(FunctionsComponent component,
        DrawActionDelegate<DrawingOptions>? next)
        : base(next, component)
    {
    }
}