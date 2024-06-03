using Phobos.Core.Drawing.Components;

namespace Phobos.Core.Drawing.Actions;

public class AxesAction : BaseAction
{
    public AxesAction(AxesComponent component,
        DrawActionDelegate<DrawingOptions>? next)
        : base(next, component)
    {
    }
}