document.querySelector('body').addEventListener('click', function (event) {
    window.chrome.webview.postMessage(event.target.getAttribute('name'));
});

document.addEventListener('mouseover', function (event) {
    event.target.style.outline = '3px solid red'
});

document.addEventListener('mouseout', function (event) {
    event.target.style.outline = ''
});
