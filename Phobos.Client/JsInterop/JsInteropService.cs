using Microsoft.JSInterop;
using Phobos.Client.JsInterop.Events.MouseMove;
using Phobos.Client.JsInterop.Events.Resized;
using Phobos.Client.JsInterop.Events.Wheel;

namespace Phobos.Client.JsInterop;

public class JsInteropService : IDisposable
{
    private readonly IJSRuntime _js;
    private readonly MouseMoveJsEventHandler _mouseMoveHandler;
    private readonly ResizedJsEventHandler _resizedHandler;
    private readonly WheelJsEventHandler _wheelHandler;

    public JsInteropService(
        IJSRuntime js,
        MouseMoveJsEventHandler mouseMoveHandler,
        ResizedJsEventHandler resizedHandler,
        WheelJsEventHandler wheelHandler)
    {
        _js = js;
        _mouseMoveHandler = mouseMoveHandler;
        _resizedHandler = resizedHandler;
        _wheelHandler = wheelHandler;
    }
    
    public async Task RegisterDotNetEventHandlers()
    {
        await _js.InvokeVoidAsync("registerDotNetMouseMoveHandler", DotNetObjectReference.Create(_mouseMoveHandler));
        await _js.InvokeVoidAsync("registerDotNetResizedHandler", DotNetObjectReference.Create(_resizedHandler));
        await _js.InvokeVoidAsync("registerDotNetWheelHandler", DotNetObjectReference.Create(_wheelHandler));
    }

    public async Task RegisterJsMouseEventHandlers(string canvasId)
    {
        await _js.InvokeVoidAsync("registerMouseEventHandlers", canvasId);
    }
    
    public async Task RegisterJsResizeHandlers(string canvasId)
    {
        await _js.InvokeVoidAsync("registerResizeHandlers", canvasId);
    }
    
    public async Task RegisterJsWheelHandlers(string canvasId)
    {
        await _js.InvokeVoidAsync("registerWheelHandlers", canvasId);
    }
    
    public async ValueTask<ElementSize> GetCanvasSize(string canvasContainerId)
    {
        return await _js.InvokeAsync<ElementSize>(identifier: "getCanvasSize", canvasContainerId);
    }
    
    public async ValueTask SetImage(string imageElementId, string imageBase64)
    {
        await _js.InvokeVoidAsync(identifier: "setImage", imageElementId, imageBase64);
    }
    
    public void Dispose()
    {
        // The following prevents derived types that introduce a
        // finalizer from needing to re-implement IDisposable.
        GC.SuppressFinalize(this);
    }
}