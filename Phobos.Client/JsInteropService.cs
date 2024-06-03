using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Phobos.Client;

public class JsInteropService : IDisposable
{
    private readonly IJSRuntime _js;
    private readonly Lazy<ValueTask<IJSObjectReference>> _module;
    
    public JsInteropService(IJSRuntime js)
    {
        _js = js;
        _module = new(() =>
            _js.InvokeAsync<IJSObjectReference>(identifier: "import", "script.js"), isThreadSafe: true);
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