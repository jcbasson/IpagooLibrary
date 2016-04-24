define(["Config/book-RequestConfig", "Store/books-store"],
function (iRequestConfig, iBookStore) {
    var bookGridModule = function (sandbox) {
        var thisModule, filterUtility, bookGridContainer, bookGridTemplate, allbtnBorrowBook;

        return {
            init: function () {
                thisModule = this;

                bookGridContainer = sandbox.find("#book-grid-container")[0];
                bookGridTemplate = sandbox.find("#book-grid-template")[0];
                
                sandbox.listen({
                    'init-book-grid': thisModule.initializeBookGrid,
                    'empty-book-grid': thisModule.emptyBookGrid
                });
            },
            destroy: function () {
                sandbox.ignore(["init-book-grid", "empty-book-grid"]);

            },
            initializeBookGrid: function () {
              
                var bookGetUrl = iRequestConfig.getBookGetEndpoint();

                var bookRequest = sandbox.httpGet(bookGetUrl);

                bookRequest.done(function (response) {
                    console.log(response);
                    if (response) {

                        iBookStore.setCurrentBook(response.Books);

                        thisModule.displayBookResult(response.Books)

                        sandbox.notify({
                            type: "load-pager",
                            data: response.Pager
                        });
                    } else {
                        sandbox.replaceContent(bookGridContainer, "<p>No books were found</p>");
                        sandbox.notify({
                            type: "load-pager",
                        });
                    }

                }).fail(function (jqXHr, textStatus, errorThrown) {
                    
                    sandbox.notify({
                        type: "alert-danger",
                        data: "Unable to access the server."
                    });
                   
                });
            },
            emptyBookGrid: function () {
                sandbox.replaceContent(bookGridContainer, "");
            },
            displayBookResult: function (books) {

                if (books && books.length > 0) {

                    var jsonStringBooks = JSON.stringify(books);
                    var jsonBooks = JSON.parse(jsonStringBooks);

                    var template = sandbox.getTemplate(bookGridTemplate);

                    var generatedHtml = template(jsonBooks);
                    sandbox.replaceContent(bookGridContainer, generatedHtml);
                    thisModule.initializeBorrowClickEvents();

                } else {
                    sandbox.replaceContent(bookGridContainer, "<p>No books were found</p>");
                }
            },
            initializeBorrowClickEvents: function () {
                var btnBorrowBook;
                allbtnBorrowBook = sandbox.find(".btnBorrowBook");
                var count = 0;

                if (allbtnBorrowBook && allbtnBorrowBook.length > 0) {
                    for (; count < allbtnBorrowBook.length; count++) {

                        btnBorrowBook = allbtnBorrowBook[count];
                        if (btnBorrowBook) {
                            sandbox.addEvent(btnBorrowBook, "click", thisModule.initializeBorrowBookForm);
                        }
                        
                    }
                }
            },
            destroyBorrowClickEvents: function () {

                if (allbtnBorrowBook && allbtnBorrowBook.length > 0) {
                    var count = 0;
                    for (; count < allbtnBorrowBook.length; count++) {

                        var btnEditPerson = allbtnBorrowBook[count];
                        sandbox.removeEvent(btnBorrowBook, "click", thisModule.initializeBorrowBookForm);
                    }
                }
            },
            initializeBorrowBookForm: function (e) {

                var count = 0;
                var clickedBorrowButton = e.currentTarget;


                var isbn = sandbox.getAttr(clickedBorrowButton, "data-isbn");
                var lenderId = sandbox.getAttr(clickedBorrowButton, "data-lenderId");

                sandbox.notify({
                    type: "init-lendbook-form",
                    data: {
                        ISBN: isbn,
                        LenderID: lenderId
                    }
                });  
            }
        }
    }
    return bookGridModule;
});