using Microsoft.Extensions.Options;
using Phobos.Core.Drawing.Configuration;
using SixLabors.ImageSharp.PixelFormats;
using Color = SixLabors.ImageSharp.Color;

namespace Phobos.Core.Drawing.Components;

public class AxesComponent : IDrawingComponent<DrawingOptions>
{
    private readonly DrawingConfigurationOptions _config;
    private readonly Color _color;
    
    public AxesComponent(IOptions<DrawingConfigurationOptions> config)
    {
        _config = config.Value;
        var rgb = _config.AxisColor;
        _color = Color.FromRgb(
            rgb.R, 
            rgb.G, 
            rgb.B);
    }
    
    public void Draw(DrawingOptions options)
    {
        // take the Y coordinate of the graph origin and draw the X axis at that Y
        if (PixelHelper.IsPixelWithinImageBounds(options.Origin.Y, options.Image.Height))
        {
            DrawXAxis(options);
        }

        if (PixelHelper.IsPixelWithinImageBounds(options.Origin.X, options.Image.Width))
        {
            DrawYAxis(options);
        }
    }

    private void DrawYAxis(DrawingOptions options)
    {
        options.Image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                Span<Rgba32> pixelRow = accessor.GetRowSpan(y);

                pixelRow[options.Origin.X] = _color;
            }
        });
    }

    private void DrawXAxis(DrawingOptions options)
    {
        options.Image.ProcessPixelRows(accessor =>
        {
            Span<Rgba32> pixelRow = accessor.GetRowSpan(options.Origin.Y);

            for (int x = 0; x < accessor.Width; x++)
            {
                pixelRow[x] = _color;
            }
        });
    }
}