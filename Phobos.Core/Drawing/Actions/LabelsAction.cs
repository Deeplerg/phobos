using Phobos.Core.Drawing.Components;

namespace Phobos.Core.Drawing.Actions;

public class LabelsAction : BaseAction
{
    public LabelsAction(LabelsComponent component,
        DrawActionDelegate<DrawingOptions>? next)
        : base(next, component)
    {
    }
}