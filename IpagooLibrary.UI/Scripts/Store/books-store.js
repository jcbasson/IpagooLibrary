define(["underscore", "jquery", "Config/book-RequestConfig"],
function (iUnderscore, iJquery, iRequestConfig) {


    var crntBook = [];

    var bookRepository = {
        setCurrentBook: function (book) {

            if (book) {
                crntBook = book;
            }
        },
        getBookromCurrentBook: function (isbn) {
           
            if (isbn) {

               
                return iUnderscore.findWhere(crntBook, { ISBN: isbn });
            }
        }
    }

    return bookRepository;
});