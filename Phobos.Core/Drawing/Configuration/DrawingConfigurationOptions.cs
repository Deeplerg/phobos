namespace Phobos.Core.Drawing.Configuration;

public class DrawingConfigurationOptions
{
    public Rgb AxisColor { get; set; }
    public Rgb TickColor { get; set; }
    public int TickSizeInPixels { get; set; }
    public string PathToFont { get; set; }
    public int FontSize { get; set; }
    public int PixelsPerValue { get; set; }
    public Rgb TextColor { get; set; }
    public Rgb BackgroundColor { get; set; }

    public const string Position = "drawingConfigurationOptions";
}

public class Rgb
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
}