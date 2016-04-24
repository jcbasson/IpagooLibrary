define(["Config/book-RequestConfig", "Store/books-store"],
function (iRequestConfig, iBookStore) {
    var bookGridModule = function (sandbox, connection) {
        var thisModule, filterUtility, bookGridContainer, bookGridTemplate, allbtnBorrowBook, allbtnReturnBook, hubProxy;

        return {
            init: function () {
                thisModule = this;

                bookGridContainer = sandbox.find("#book-grid-container")[0];
                bookGridTemplate = sandbox.find("#book-grid-template")[0];

                sandbox.listen({
                    'init-book-grid': thisModule.initializeBookGrid,
                    'empty-book-grid': thisModule.emptyBookGrid
                });

                hubProxy = connection.createHubProxy('LibraryHub');

                hubProxy.on('BookReturnedResult', function (data) {

                    thisModule.processBookReturnedResult(data);

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
                    thisModule.initializeReturnClickEvents();

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

                        var btnBorrowBook = allbtnBorrowBook[count];
                        sandbox.removeEvent(btnBorrowBook, "click", thisModule.initializeBorrowBookForm);
                    }
                }
            },
            initializeReturnClickEvents: function () {

                connection.start().done(function () {

                    var btnReturnBook;
                    allbtnReturnBook = sandbox.find(".btnReturnBook");
                    var count = 0;

                    if (allbtnReturnBook && allbtnReturnBook.length > 0) {
                        for (; count < allbtnReturnBook.length; count++) {

                            btnReturnBook = allbtnReturnBook[count];
                            if (btnReturnBook) {
                                sandbox.addEvent(btnReturnBook, "click", thisModule.returnBook);
                            }

                        }
                    }
                });
            },
            destroyReturnClickEvents: function () {

                if (allbtnReturnBook && allbtnReturnBook.length > 0) {
                    var count = 0;
                    for (; count < allbtnReturnBook.length; count++) {

                        var btnReturnBook = allbtnReturnBook[count];
                        sandbox.removeEvent(btnReturnBook, "click", thisModule.returnBook);
                    }
                }
            },
            initializeBorrowBookForm: function (e) {

                var clickedBorrowBookButton = e.currentTarget;

                var isbn = sandbox.getAttr(clickedBorrowBookButton, "data-isbn");
                var lenderId = sandbox.getAttr(clickedBorrowBookButton, "data-lenderId");

                sandbox.notify({
                    type: "init-lendbook-form",
                    data: {
                        ISBN: isbn,
                        LenderID: lenderId
                    }
                });
            },
            returnBook: function (e) {

                var clickedReturnBookButton = e.currentTarget;

                var isbn = sandbox.getAttr(clickedReturnBookButton, "data-isbn");
                var lenderId = sandbox.getAttr(clickedReturnBookButton, "data-lenderId");

                var returnedBook = {
                    ISBN: isbn,
                    LenderID: lenderId
                }
                hubProxy.invoke('ReturnBook', returnedBook).fail(function (e) {
                                       
                    sandbox.notify({
                        type: "alert-danger",
                        data: "Unable to access the server."
                    });
                });;

            },
            processBookReturnedResult: function (result) {

                var bookISBN = result.BookISBN;

                sandbox.notify({
                    type: "alert-success",
                    data: "Book with ISBN number " + bookISBN + " has just become available."
                });
            }
        }
    }
    return bookGridModule;
});