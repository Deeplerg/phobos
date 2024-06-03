using Microsoft.Extensions.Options;
using Phobos.Core.Drawing.Configuration;
using SixLabors.ImageSharp.PixelFormats;
using Color = SixLabors.ImageSharp.Color;

namespace Phobos.Core.Drawing.Components;

public class TicksComponent : IDrawingComponent<DrawingOptions>
{
    private readonly DrawingConfigurationOptions _config;
    private readonly Color _color;
    private int _step;
    
    public TicksComponent(IOptions<DrawingConfigurationOptions> config)
    {
        _config = config.Value;
        
        var rgb = _config.TickColor;
        _color = Color.FromRgb(
            rgb.R, 
            rgb.G, 
            rgb.B);
    }
    
    public void Draw(DrawingOptions options)
    {
        _step = PixelHelper.CalculateStep(options.ScaleFactor, _config.PixelsPerValue);
        
        DrawTicksOnYAxis(options);
        DrawTicksOnXAxis(options);
    }

    private void DrawTicksOnYAxis(DrawingOptions options)
    {
        options.Image.ProcessPixelRows(accessor =>
        {
            int originY = Math.Clamp(options.Origin.Y, 0, accessor.Height);

            for (int y = originY + _step; y < accessor.Height; y += _step)
            {
                Span<Rgba32> pixelRow = accessor.GetRowSpan(y);

                DrawTickOnYAxis(ref pixelRow, options);
            }

            for (int y = originY - _step; y >= 0; y -= _step)
            {
                Span<Rgba32> pixelRow = accessor.GetRowSpan(y);

                DrawTickOnYAxis(ref pixelRow, options);
            }
        });
    }

    private void DrawTickOnYAxis(ref Span<Rgba32> pixelRow, DrawingOptions options)
    {
        int imageWidth = pixelRow.Length;
        
        int startPointX = Math.Clamp(
            options.Origin.X - _config.TickSizeInPixels,
            0, 
            imageWidth - _config.TickSizeInPixels);
        
        int endPointX = Math.Clamp(
            options.Origin.X + _config.TickSizeInPixels, 
            _config.TickSizeInPixels - 1, 
            imageWidth - 1);

        DrawHorizontalTick(ref pixelRow, startPointX, endPointX);
    }

    private void DrawHorizontalTick(ref Span<Rgba32> pixelRow, int fromPixelIndex, int toPixelIndex)
    {
        for (int x = fromPixelIndex; x < toPixelIndex + 1; x++)
        {
            pixelRow[x] = _color;
        }
    }

    private void DrawTicksOnXAxis(DrawingOptions options)
    {
        options.Image.ProcessPixelRows(accessor =>
        {
            int startPointY = Math.Clamp(
                options.Origin.Y - _config.TickSizeInPixels, 
                0, 
                options.Image.Height - _config.TickSizeInPixels);
            
            int endPointY = Math.Clamp(
                options.Origin.Y + _config.TickSizeInPixels, 
                _config.TickSizeInPixels - 1, 
                options.Image.Height - 1);

            for (int y = startPointY; y <= endPointY; y++)
            {
                Span<Rgba32> pixelRow = accessor.GetRowSpan(y);

                int originX = Math.Clamp(options.Origin.X, 0, pixelRow.Length);

                for (int x = originX - _step; x >= 0; x -= _step)
                {
                    pixelRow[x] = _color;
                }
                for (int x = originX + _step; x < pixelRow.Length; x += _step)
                {
                    pixelRow[x] = _color;
                }
            }
        });
    }
}