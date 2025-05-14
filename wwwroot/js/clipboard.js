window.clipboardCopy = {
    copyText: function(text) {
        navigator.clipboard.writeText(text).then(function () {
            console.log("Text copied");
        })
        .catch(function (error) {
            console.error("Copy failed", error);
        });
    }
};