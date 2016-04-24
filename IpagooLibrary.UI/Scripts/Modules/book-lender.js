﻿define(function () {
    var bookLenderModule = function (sandbox, connection) {
        var thisModule, selectedBookISBN, bookLenderFormModal, txtFriendName, txtBorrowedDate, txtComments, lblISBN, btnSubmitLender, hubProxy;

        return {
            init: function () {
                thisModule = this;

                bookLenderFormModal = sandbox.find("#book-lenderform-modal")[0];
                txtFriendName = sandbox.find("#txtFriendName")[0];
                txtBorrowedDate = sandbox.find("#txtBorrowedDate")[0];

                sandbox.initializeDatePicker(sandbox.find("#datetimepickerBorrowDate"));
                txtComments = sandbox.find("#txtComments")[0];
                lblISBN = sandbox.find("#lblISBN")[0];
                btnSubmitLender = sandbox.find("#btnSubmitLender")[0];

                sandbox.listen({
                    'init-lendbook-form': thisModule.initializeLendBookForm,
                    'empty-lendbook-form': thisModule.emptyLendBookForm
                });

                sandbox.addEvent(txtFriendName, "keyup", thisModule.validateFriendsName);
                sandbox.addEvent(txtBorrowedDate, "keyup", thisModule.validateBorrowDate);


                hubProxy = connection.createHubProxy('LibraryHub');

                hubProxy.on('BookLenderCreateResult', function (data) {
                    
                    thisModule.processBookLenderCreateResult(data);

                });

                connection.start().done(function () {

                    sandbox.addEvent(btnSubmitLender, "click", thisModule.submitBookLender);
                });
            },
            destroy: function () {
                sandbox.ignore(["init-lendbook-form", "empty-lendbook-form"]);
            },
            initializeLendBookForm: function (data) {

                if (data) {

                    selectedBookISBN = data.ISBN;
                    lblISBN.innerHTML = data.ISBN;
                    sandbox.showModal(bookLenderFormModal);
                    thisModule.emptyLendBookForm();
                }
            },
            emptyLendBookForm: function () {

                txtFriendName.value = "";
                txtBorrowedDate.value = "";
                txtComments.value = "";

                sandbox.removeClass(txtFriendName, "alert-danger");
                sandbox.removeClass(txtBorrowedDate, "alert-danger");
            },
            submitBookLender: function () {

                if (selectedBookISBN
                    && thisModule.validateInput(txtFriendName)
                    && thisModule.validateInput(txtBorrowedDate)) {

                    var bookLender = {
                        FriendName: txtFriendName.value,
                        BookISBN: selectedBookISBN,
                        BorrowDate: txtBorrowedDate.value,
                        Comments: txtComments.value,
                    };

                    hubProxy.invoke('CreateBookLender', bookLender).fail(function (e) {

                        sandbox.hideModal(bookLenderFormModal);
                        sandbox.notify({
                            type: "alert-danger",
                            data: "Unable to access the server."
                        });
                    });;
                }
            },
            validateFriendsName: function () {
                thisModule.validateInput(txtFriendName);

            },
            validateBorrowDate: function () {
                thisModule.validateInput(txtBorrowedDate);
            },
            validateInput: function (inputControl) {
                if (!inputControl.value) {
                    sandbox.addClass(inputControl, "alert-danger");
                    return false;
                } else {
                    sandbox.removeClass(inputControl, "alert-danger");
                    return true;
                }
            },
            processBookLenderCreateResult: function (result) {

                if (result && result.Status === "Validation Error") {
                    thisModule.validateFriendsName();
                    thisModule.validateBorrowDate();
                }
                else if(result.Status === "Success"){

                    var bookISBN = result.BookISBN;

                    sandbox.hideModal(bookLenderFormModal);

                    sandbox.notify({
                        type: "alert-info",
                        data: "Book with ISBN number " + bookISBN + " was checked out."
                    });
                }
            }
        }
    }
    return bookLenderModule;
})