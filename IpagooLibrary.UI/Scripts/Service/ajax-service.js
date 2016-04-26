define(["jquery"],
function (iJquery) { 
    return {
        HttpGet: function (url) {
            
            return iJquery.get(url);
        },
        HttpPost: function (url, data) {
            
            var jsonData = JSON.stringify(data);
            // return iJquery.post(url, data);
            return iJquery.ajax({
                url: url,
                method: "POST",
                data: jsonData,
                async :true,
                contentType: "application/json; charset=utf-8",
            });
        }
    }
});
