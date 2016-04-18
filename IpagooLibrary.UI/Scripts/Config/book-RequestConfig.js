define(function () {
    var bookFilterConfig = function () {
        var thisModule, ISBNFilter, titleFilter, authorFilter, genreFilter, apiEndPoint = "GetBooks", bookOffset = 0, bookDisplayLimitPerPage = 33, numberOfPagerButtonDisplayed = 6;
        return {
            updateOffset: function (offset) {
                bookOffset = offset;
            },
            updateISBNFilter: function (isbn) {
               
                ISBNFilter = isbn;
            },
            updateTitleFilter: function (title) {

                titleFilter = title;
            },
            updateAuthorFilter: function (author) {

                authorFilter = author;
            },
            updateGenreFilter: function (genre) {

                genreFilter = genre;
            },
            getBookDisplayedPerPage(){
                return bookDisplayLimitPerPage;
            },
            getNumberOfPagerButtonDisplayed(){
                return numberOfPagerButtonDisplayed;
            },
            getBookGetEndpoint: function () {

                var bookGetUrl = apiEndPoint + "?Limit=" + bookDisplayLimitPerPage +
                    "&Offset=" + bookOffset;
             
                if (ISBNFilter) {
                    bookGetUrl += "&ISBN=" + ISBNFilter;
                }

                if (titleFilter) {
                    bookGetUrl += "&Title=" + titleFilter;
                }

                if (authorFilter) {
                    bookGetUrl += "&AuthorName=" + authorFilter;
                }

                if (genreFilter) {
                    bookGetUrl += "&Genre=" + genreFilter;
                }

                return bookGetUrl;
            },
            getBookPostEndPoint: function () {
                return apiEndPoint;
            }
        }
    }
    return new bookFilterConfig();
});