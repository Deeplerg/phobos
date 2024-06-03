using ExpressionCalculator.Tokenization.Tokens;
using Microsoft.Extensions.Options;
using Phobos.Core.Drawing.Configuration;
using Phobos.Core.UnknownVariable;

namespace Phobos.Core.Drawing.Components;

public class FunctionsComponent : IDrawingComponent<DrawingOptions>
{
    private readonly DrawingConfigurationOptions _config;
    private readonly ExpressionCalculator.ExpressionCalculator _calculator;
    
    public FunctionsComponent(
        IOptions<DrawingConfigurationOptions> config,
        ExpressionCalculator.ExpressionCalculator calculator)
    {
        _config = config.Value;
        _calculator = calculator;
    }

    public void Draw(DrawingOptions options)
    {
        foreach (var function in options.Functions)
        {
            for (int x = 0; x < options.Image.Width; x++)
            {
                double xCoordinate = 
                    PixelHelper.CalculateCoordinateValueAtPixel(
                        x, options.Origin.X, _config.PixelsPerValue, options.ScaleFactor);

                var tokenRequest = _calculator.ConvertToReversePolishNotation(function.Input);
                var tokens = tokenRequest
                    .Select(t => t is UnknownVariableToken ? new NumberToken(xCoordinate) : t)
                    .ToList();

                double result = _calculator.Calculate(tokens);

                int xPixel = PixelHelper.CalculatePixelAtCoordinate(xCoordinate, options.Origin.X, _config.PixelsPerValue, options.ScaleFactor);
                int yPixel = PixelHelper.CalculatePixelAtCoordinate(0 - result, options.Origin.Y, _config.PixelsPerValue, options.ScaleFactor);

                if (PixelHelper.IsPixelWithinImageBounds(yPixel, options.Image.Height))
                    options.Image[xPixel, yPixel] = function.Color;
            }
        }
    }
}