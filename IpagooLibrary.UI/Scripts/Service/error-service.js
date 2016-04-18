define(["Service/ajax-service"],
function (iAjaxService) {
    var that = this;
    that.httpService = iAjaxService;
    window.onerror = function (errorMessage, sourceScript, sourceLineNumber, sourceColumn, stackTrace) {
        alert("Error: " + errorMessage + " Script: " + sourceScript + " Line: " + sourceLineNumber + " Column: " + sourceColumn + " StackTrace: " + stackTrace);
        var url = "/ClientException/Log";
        var clientException = {
            ErrorMessage: errorMessage,
            SourceScript: sourceScript,
            SourceLineNumber: sourceLineNumber,
            SourceColumn: sourceColumn,
            StackTrace: stackTrace
        };
        //TODO: Log this error to a database the UI uses and not the api which it interactw with
        that.httpService.HttpPost(url, clientException).done(function () {
            console.log("Client error was logged");
        });
    }
});