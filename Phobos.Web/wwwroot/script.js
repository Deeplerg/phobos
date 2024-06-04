let isMouseDown = false;
let dotnetMouseMoveHandler;
let dotnetResizedHandler;
let dotnetWheelHandler;

function getCanvasSize(canvasContainerId) {
    let element = document.getElementById(canvasContainerId);
    
    return getElementSize(element);
}

function getElementSize(element) {
    let width = element.clientWidth;
    let height = element.clientHeight;

    return {
        width: width,
        height: height
    };
}

function setImage(imageElementId, base64) {
    const image = document.getElementById(imageElementId);
    image.src = base64;
}

function onMouseDown(event) {
    isMouseDown = true;
}

function onMouseUp(event) {
    isMouseDown = false;
}

function onMouseMove(event) {
    let dotnetMouseMoveEvent = {
        clientX: event.clientX,
        clientY: event.clientY,
        isDown: isMouseDown
    }

    invokeDotNetHandler(dotnetMouseMoveHandler, dotnetMouseMoveEvent);
}

function registerDotNetMouseMoveHandler(handler) {
    dotnetMouseMoveHandler = handler;
}

function registerDotNetResizedHandler(handler) {
    dotnetResizedHandler = handler;
}

function registerDotNetWheelHandler(handler) {
    dotnetWheelHandler = handler;
}

function registerMouseEventHandlers(canvasId) {
    let element = document.getElementById(canvasId);
    
    element.onmousedown = onMouseDown;
    element.onmousemove = onMouseMove;
    document.onmouseup = onMouseUp;
}

function registerResizeHandlers(canvasId) {
    let element = document.getElementById(canvasId);

    let resizeObserver = new ResizeObserver(onResized);
    resizeObserver.observe(element);
}

function onResized(entries, observer) {
    invokeDotNetHandler(dotnetResizedHandler, { });
}

function registerWheelHandlers(canvasId) {
    let element = document.getElementById(canvasId);
    
    element.onwheel = onWheel;
}

function onWheel(event) {
    let dotnetWheelEvent = {
        deltaX: event.deltaX,
        deltaY: event.deltaY,
        deltaMode: event.deltaMode
    }
    
    invokeDotNetHandler(dotnetWheelHandler, dotnetWheelEvent);
}

function invokeDotNetHandler(handler, event) {
    handler.invokeMethodAsync('HandleAsync', event);
}