using Microsoft.Extensions.Options;
using Phobos.Core.Drawing.Configuration;
using Phobos.Core.Drawing.Pipeline;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using Point = System.Drawing.Point;
using Color = SixLabors.ImageSharp.Color;

namespace Phobos.Core.Drawing;

public class GraphCanvas
{
    private readonly IDrawingPipeline<DrawingOptions> _pipeline;
    private readonly DrawingConfigurationOptions _config;
    private readonly Color _backgroundColor;
    private readonly List<Function> _functions = new();
    private Image<Rgba32>? _image;

    public GraphCanvas(
        IDrawingPipeline<DrawingOptions> pipeline,
        IPipelineActionsCompositor pipelineSetup,
        IOptions<DrawingConfigurationOptions> config)
    {
        _pipeline = pipeline;
        _config = config.Value;

        var rgb = _config.BackgroundColor;
        _backgroundColor = Color.FromRgb(
            rgb.R,
            rgb.G,
            rgb.B);
        
        pipelineSetup.SetupPipeline(_pipeline);
    }

    public void AddFunction(Function function)
        => _functions.Add(function);

    public void RemoveFunction(Function function)
        => _functions.Remove(function);

    public void ClearFunctions()
        => _functions.Clear();

    public void Draw(int width, int height, Point origin, double scaleFactor = 1)
    {
        _image?.Dispose();

        var image = new Image<Rgba32>(width, height, _backgroundColor);

        var options = new DrawingOptions
        {
            Image = image,
            Functions = _functions,
            Origin = origin,
            ScaleFactor = scaleFactor
        };
        
        _pipeline.Draw(options);
        
        _image = image;
    }

    public string SaveAsBase64()
    {
        if (_image is null)
            throw new InvalidOperationException($"{Draw} has not been called");

        return _image.ToBase64String(PngFormat.Instance);
    }

    public void SaveToStream(Stream stream)
    {
        if (_image is null)
            throw new InvalidOperationException($"{Draw} has not been called");

        _image.Save(stream, PngFormat.Instance);
    }

    public void Dispose()
    {
        _image?.Dispose();
    }
}
