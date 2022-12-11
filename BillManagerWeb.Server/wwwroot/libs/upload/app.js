export function init(wrapper, element, inputFile) {
    // 阻止浏览器默认行为
    document.addEventListener("drag", function (e) {
        e.preventDefault();
    }, false);
    document.addEventListener("dragleave", function (e) {
        e.preventDefault();
    }, false);
    document.addEventListener("drop", function (e) {
        e.preventDefault();
        return false;//禁止浏览器默认行为
    }, false);
    document.addEventListener("dragenter", function (e) {
        e.preventDefault();
    }, false);
    document.addEventListener("dragover", function (e) {
        e.preventDefault();
    }, false);

    element.addEventListener("drop", function (e) {
        try {
            var fileList = e.dataTransfer.files; //获取文件对象
            //检测是否是拖拽文件到页面的操作
            if (fileList.length === 0) {
                return false;
            }

            inputFile.files = e.dataTransfer.files;
            const event = new Event('change', {bubbles: true});
            inputFile.dispatchEvent(event);
        } catch (e) {
            wrapper.invokeMethodAsync('DropAlert', e);
        }
    }, false);

    element.addEventListener('paste', function (e) {
        inputFile.files = e.clipboardData.files;
        const event = new Event('change', {bubbles: true});
        inputFile.dispatchEvent(event);
    }, false);


    return {
        dispose: () => {
            element.removeEventListener('drag', ondrag);
            element.removeEventListener('dragleave', ondragleave);
            element.removeEventListener("drop", ondrop);
            element.removeEventListener('dragenter', ondragenter);
            element.removeEventListener('dragover', ondragover);
            element.removeEventListener('paste', document.onpaste);
        }
    }

}