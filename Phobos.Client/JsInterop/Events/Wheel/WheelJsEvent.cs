namespace Phobos.Client.JsInterop.Events.Wheel;

public record class WheelJsEvent(double DeltaX, double DeltaY, ulong DeltaMode);