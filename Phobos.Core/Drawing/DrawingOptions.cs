using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Point = System.Drawing.Point;

namespace Phobos.Core.Drawing;

public class DrawingOptions
{
    public DrawingOptions() { }
    
    public DrawingOptions(Image<Rgba32> image, 
        Point origin, 
        double scaleFactor,
        List<Function> functions)
    {
        Image = image;
        Origin = origin;
        ScaleFactor = scaleFactor;
        Functions = functions;
    }

    public Image<Rgba32> Image { get; set; }
    public Point Origin { get; set; }
    public double ScaleFactor { get; set; }
    public List<Function> Functions { get; set; }
}