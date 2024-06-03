using Phobos.Core.Drawing.Actions;

namespace Phobos.Core.Drawing.Pipeline;

public class PipelineActionsCompositor : IPipelineActionsCompositor
{
    public void SetupPipeline(IDrawingPipeline<DrawingOptions> pipeline)
    {
        pipeline.AddAction<AxesAction>();
        pipeline.AddAction<TicksAction>();
        pipeline.AddAction<LabelsAction>();
        pipeline.AddAction<FunctionsAction>();
    }
}