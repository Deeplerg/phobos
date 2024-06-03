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