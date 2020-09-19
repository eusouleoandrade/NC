function setTempDataMessage(message) {
    var tempDataMessage = message;

    if (tempDataMessage != '') {
        setToastr();
        toastr.success(tempDataMessage, 'Notification');
    };
}

function setTempDataMessageError(message) {
    var tempDataMessage = message;

    if (tempDataMessage != '') {
        setToastr();
        toastr.error(tempDataMessage, 'Error');
    };
}