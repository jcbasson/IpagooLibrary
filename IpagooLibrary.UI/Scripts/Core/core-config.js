define(function() {

    var config = function() {

        var apiBaseUrl = "/Home/";

        return {
            getApiBaseUrl: function() {
                return apiBaseUrl;
            }
        }
    };
    return config;
});