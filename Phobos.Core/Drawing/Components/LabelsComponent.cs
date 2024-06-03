using Microsoft.Extensions.Options;
using Phobos.Core.Drawing.Configuration;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using Color = SixLabors.ImageSharp.Color;
using Font = SixLabors.Fonts.Font;
using PointF = SixLabors.ImageSharp.PointF;

namespace Phobos.Core.Drawing.Components;

public class LabelsComponent : IDrawingComponent<DrawingOptions>
{
    private readonly DrawingConfigurationOptions _config;
    private readonly Color _color;
    private Font _font;
    private TextOptions _textOptions;
    private int _spacing;
    private int _step;
    
    public LabelsComponent(IOptions<DrawingConfigurationOptions> config)
    {
        _config = config.Value;

        var rgb = _config.TextColor;
        _color = Color.FromRgb(
            rgb.R, 
            rgb.G, 
            rgb.B);
    }
    
    public void Draw(DrawingOptions options)
    {
        var fontCollection = new FontCollection();
        var fontFamily = fontCollection.Add(_config.PathToFont);
        _font = fontFamily.CreateFont(_config.FontSize);

        _textOptions = new TextOptions(_font);

        _spacing = _config.TickSizeInPixels * 2;
        _step = PixelHelper.CalculateStep(options.ScaleFactor, _config.PixelsPerValue);

        options.Image.Mutate(context =>
        {
            var imageSize = context.GetCurrentSize();

            DrawXAxisLabels(context, options);
            DrawYAxisLabels(context, options);
        });
    }

    private void DrawXAxisLabels(IImageProcessingContext context, DrawingOptions options)
    {
        int imageWidth = options.Image.Width;

        // positive x axis
        int labelX = options.Origin.X + _config.PixelsPerValue;
        for (int x = options.Origin.X + _step; x < imageWidth; x += _step)
        {
            DrawXLabel(context, options, x, labelX);
            labelX += _config.PixelsPerValue;
        }

        // negative x axis
        labelX = options.Origin.X - _config.PixelsPerValue;
        for (int x = options.Origin.X - _step; x >= 0; x -= _step)
        {
            DrawXLabel(context, options, x, labelX);
            labelX -= _config.PixelsPerValue;
        }
    }

    private void DrawXLabel(IImageProcessingContext context, DrawingOptions options, int x, int labelX)
    {
        var scale = CalculateScaleForLabel(options);

        double xValue = PixelHelper.CalculateCoordinateValueAtPixel(labelX, options.Origin.X, _config.PixelsPerValue, scale);
        string text = xValue.ToString();

        var textRectangle = TextMeasurer.MeasureSize(text, _textOptions);

        int xTextCoordinate = x - (int)textRectangle.Width / 2;
        int yTextCoordinate = options.Origin.Y + _spacing;

        if (!PixelHelper.IsPointWithinImageBounds(
               xTextCoordinate,
               yTextCoordinate,
               options.Image.Width,
               options.Image.Height))
        {
            return;
        }

        var location = new PointF(xTextCoordinate, yTextCoordinate);

        context.DrawText(text, _font, _color, location);
    }

    private static double CalculateScaleForLabel(DrawingOptions options)
    {
        double scale;
        if (options.ScaleFactor >= 1)
        {
            scale = Math.Truncate(options.ScaleFactor);
        }
        else
        {
            scale = 1 / Math.Truncate(1 / options.ScaleFactor);
        }

        return scale;
    }

    private void DrawYAxisLabels(IImageProcessingContext context, DrawingOptions options)
    {
        int imageHeight = options.Image.Height;

        // negative y axis
        int labelY = options.Origin.Y + _config.PixelsPerValue;
        for (int y = options.Origin.Y + _step; y < imageHeight; y += _step)
        {
            DrawYLabel(context, options, y, labelY);
            labelY += _config.PixelsPerValue;
        }

        // positive y axis
        labelY = options.Origin.Y - _config.PixelsPerValue;
        for (int y = options.Origin.Y - _step; y >= 0; y -= _step)
        {
            DrawYLabel(context, options, y, labelY);
            labelY -= _config.PixelsPerValue;
        }
    }

    private void DrawYLabel(IImageProcessingContext context, DrawingOptions options, int y, int labelY)
    {
        double scale = CalculateScaleForLabel(options);

        double yValue = PixelHelper.CalculateCoordinateValueAtPixel(labelY, options.Origin.Y, _config.PixelsPerValue, scale);
        yValue = -yValue; // y begins at the top and goes down in the image
        string text = yValue.ToString();

        var textRectangle = TextMeasurer.MeasureSize(text, _textOptions);

        int xTextCoordinate = options.Origin.X - _spacing - (int)textRectangle.Width;
        int yTextCoordinate = y - (int)textRectangle.Height / 2;

        if (!PixelHelper.IsPointWithinImageBounds(
               xTextCoordinate,
               yTextCoordinate,
               options.Image.Width,
               options.Image.Height))
        {
            return;
        }

        var location = new PointF(xTextCoordinate, yTextCoordinate);

        context.DrawText(text, _font, _color, location);
    }
}