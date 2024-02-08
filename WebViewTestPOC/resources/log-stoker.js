function getElementXPath(element) {
    if (element && element.id) {
        return 'id("' + element.id + '")';
    } else {
        return getElementTreeXPath(element);
    }
}

function getElementTreeXPath(element) {
    var paths = [];

    for (; element && element.nodeType == Node.ELEMENT_NODE; element = element.parentNode) {
        var index = 0;
        var hasFollowingSiblings = false;

        for (var sibling = element.previousSibling; sibling; sibling = sibling.previousSibling) {
            if (sibling.nodeType != Node.DOCUMENT_TYPE_NODE && sibling.nodeName == element.nodeName) {
                index++;
            }
        }

        for (var sibling = element.nextSibling; sibling && !hasFollowingSiblings; sibling = sibling.nextSibling) {
            if (sibling.nodeName == element.nodeName) {
                hasFollowingSiblings = true;
            }
        }

        var tagName = (element.prefix ? element.prefix + ":" : "") + element.localName;
        var pathIndex = (index || hasFollowingSiblings) ? "[" + (index + 1) + "]" : "";
        paths.splice(0, 0, tagName + pathIndex);
    }

    return paths.length ? "/" + paths.join("/") : null;
}

function logStoker()
{
    const callback = function (e) {
        var xpath = getElementXPath(e.target);
        console.info(e.view.location.href, e.target, e.type, xpath);
    };

    document.addEventListener('click', callback);
    document.addEventListener('change', callback);
}

logStoker();
