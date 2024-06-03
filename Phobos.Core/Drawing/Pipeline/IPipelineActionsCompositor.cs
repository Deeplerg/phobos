namespace Phobos.Core.Drawing.Pipeline;

public interface IPipelineActionsCompositor
{
    void SetupPipeline(IDrawingPipeline<DrawingOptions> pipeline);
}