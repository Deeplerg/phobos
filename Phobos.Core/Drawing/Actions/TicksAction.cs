using Phobos.Core.Drawing.Components;

namespace Phobos.Core.Drawing.Actions;

public class TicksAction : BaseAction
{
    public TicksAction(TicksComponent component,
        DrawActionDelegate<DrawingOptions>? next)
        : base(next, component)
    {
    }
}