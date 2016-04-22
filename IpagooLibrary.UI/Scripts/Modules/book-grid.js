define(["Config/book-RequestConfig", "Store/books-store"],
function (iRequestConfig, iBookStore) {
    var peopleGridModule = function (sandbox) {
        var thisModule, filterUtility, bookGridContainer, bookGridTemplate, allbtnBorrowBook;

        return {
            init: function () {
                thisModule = this;

                bookGridContainer = sandbox.find("#book-grid-container")[0];
                bookGridTemplate = sandbox.find("#book-grid-template")[0];
                
                //thisModule.initializeBookGrid();
                sandbox.listen({
                    'init-book-grid': thisModule.initializeBookGrid,
                    'empty-book-grid': thisModule.emptyBookGrid
                });
            },
            destroy: function () {
                sandbox.ignore(["init-book-grid", "empty-book-grid"]);

            },
            initializeBookGrid: function () {
                debugger;
                var bookGetUrl = iRequestConfig.getBookGetEndpoint();

                var bookRequest = sandbox.httpGet(bookGetUrl);

                bookRequest.done(function (response) {

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
                    thisModule.initializeClickEvents();

                } else {
                    sandbox.replaceContent(bookGridContainer, "<p>No books were found</p>");
                }
            },
            initializeClickEvents: function () {
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
            destroyClickEvents: function () {

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
                var clickedEditPersonButton = e.currentTarget;
     

            },
            denitializeInlineEditing: function (personId) {

                var count = 0;
                var personRow = sandbox.find("#tr" + personId)[0];

                var personRowColumns = personRow.children;

                for (; count < personRowColumns.length; count++) {
                    debugger;
                    var personColumn = personRowColumns[count];

                    var columnFor = sandbox.getAttr(personColumn, "data-for");
                                        
                    if (columnFor) {

                        var txtBoxForColumn = sandbox.find("#txt" + columnFor + "-" + personId)[0];

                        var txtBoxValue = txtBoxForColumn.value;
                        sandbox.removeAttr(personColumn, "data-val");
                        sandbox.addAttr(personColumn, { "data-val": txtBoxValue });

                        sandbox.replaceContent(personColumn, txtBoxValue);
                    }
                }
            },
            borrowBook: function (e) {
                var clickedborrowBookButton = e.currentTarget;
                var bookISBN = sandbox.getAttr(clickedborrowBookButton, "data-isbn");
                var isValidToSave = true;
                if (bookISBN) {
                    debugger;
                    var txtFriendName = sandbox.find("#txtFriendName")[0];
                    var txtBorrowedDate = sandbox.find("#txtBorrowedDate")[0];
                    var txtComments = sandbox.find("#txtComments")[0];
                  

                    var personToSave = {
                        ID: personId,
                        Firstname: txtFirstname.value,
                        Surname: txtSurname.value,
                        Gender: txtGender.value,
                        EmailAddress: txtEmailAddress.value,
                        PhoneNumber: txtPhoneNumber.value,
                    };

                    if (!personToSave.Firstname) {
                        sandbox.addClass(txtFirstname, "alert-danger");
                        isValidToSave = false;
                    } else {
                        sandbox.removeClass(txtFirstname, "alert-danger");
                    }

                    if (!personToSave.Surname) {
                        sandbox.addClass(txtSurname, "alert-danger");
                        isValidToSave = false;
                    } else {
                        sandbox.removeClass(txtSurname, "alert-danger");
                    }

                    if (!personToSave.Gender) {
                        sandbox.addClass(txtGender, "alert-danger");
                        isValidToSave = false;
                    } else {
                        sandbox.removeClass(txtGender, "alert-danger");
                    }

                    if (!personToSave.EmailAddress) {
                        sandbox.addClass(txtEmailAddress, "alert-danger");
                        isValidToSave = false;
                    } else {
                        sandbox.removeClass(txtEmailAddress, "alert-danger");
                    }

                    if (!personToSave.PhoneNumber) {
                        personToSave.PhoneNumber = "";
                    }
                   
                    if (isValidToSave) {
                                              
                        var peoplePostUrl = iRequestConfig.getPeoplePostEndPoint();

                        sandbox.httpPost(peoplePostUrl, personToSave).done(function () {
                            sandbox.notify({
                                type: "alert-success",
                                data: "The person was successfully updated."
                            });
                            sandbox.addClass(clickedborrowBookButton, "hide");
                            thisModule.denitializeInlineEditing(personId);

                        }).fail(function (response) {
                            console.log(response);

                            if (response.responseText) {
                                sandbox.notify({
                                    type: "alert-danger",
                                    data: response.responseText
                                });
                            } else {
                                sandbox.notify({
                                    type: "alert-danger",
                                    data: response.statusText
                                });
                            }
                            return;
                        });
                        
                    }
                }
            },

        }
    }
    return peopleGridModule;
});