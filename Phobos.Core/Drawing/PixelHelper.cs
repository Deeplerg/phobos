using System.Drawing;

namespace Phobos.Core.Drawing;

static class PixelHelper
{
    public static (double xValue, double yValue) CalculateValuesAtPixel(Point pixelPosition, Point origin, int pixelsPerValue, double scaling)
    {
        double xValue = CalculateCoordinateValueAtPixel(pixelPosition.X, origin.X, pixelsPerValue, scaling);
        double yValue = CalculateCoordinateValueAtPixel(pixelPosition.Y, origin.Y, pixelsPerValue, scaling);
        return (xValue, yValue);
    }

    /// <summary>
    /// Calculates the coordinate value (e.g. X or Y)
    /// at a specific pixel position on that coordinate's axis
    /// </summary>
    public static double CalculateCoordinateValueAtPixel(int pixelPosition, int axisOrigin, int pixelsPerValue, double scaling)
    {
        double distanceFromOrigin = pixelPosition - axisOrigin;
        return distanceFromOrigin / pixelsPerValue * (1d / scaling);
    }

    public static int CalculatePixelAtCoordinate(double coordinate, int axisOrigin, int pixelsPerValue, double scaling)
    {
        return (int)(coordinate * scaling * pixelsPerValue + axisOrigin);
    }

    public static bool IsPointWithinImageBounds(int pixelX, int pixelY, int imageWidth, int imageHeight)
    {
        return IsPixelWithinImageBounds(pixelX, imageWidth)
            && IsPixelWithinImageBounds(pixelY, imageHeight);
    }

    public static bool IsPixelWithinImageBounds(int pixelIndex, int imageSize)
    {
        return pixelIndex >= 0 && pixelIndex <= imageSize - 1;
    }
    
    public static int CalculateStep(double scaleFactor, int pixelsPerValue)
    {
        return (int)(pixelsPerValue * scaleFactor); // I give up
        
        int step = pixelsPerValue;
        if (scaleFactor * step >= step)
        {
            //step = (int)(step * (1d + Math.Truncate(scaleFactor)));
            step = step + (int)((scaleFactor * step) % step);
            //step = (int)(step * scaleFactor / Math.Truncate(scaleFactor));
            //step = (int)((step * scaleFactor - origin.Y + image.Height) / (Math.Truncate(scaleFactor) - (Math.Pow(2, Math.Floor(Math.Log(scaleFactor, 2)))) + 1));
            //step = (int)(step + (step * scaleFactor - step * Math.Truncate(scaleFactor)));
            //step = (int)(step * (scaleFactor / Math.Pow(2, Math.Floor(Math.Log(scaleFactor, 2)))));
            //step = (int)(step * (scaleFactor - (Math.Truncate(scaleFactor) - 1)));

            //test case: ppv = 25, scale = 66.25, origin = 1875 -> step = 27.0833332 (or *3=81.25)
        }
        else
        {
            step = (step * 2) - (int)((1 / scaleFactor * step) % step);
            //step = (int)(step * (1 / scaleFactor) / Math.Truncate(1 / scaleFactor));
        }

        return step;
    }
}
