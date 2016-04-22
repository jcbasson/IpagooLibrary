define(["Config/book-RequestConfig"],
function (iRequestConfig) {
    var bookSearchBoxModule = function (sandbox) {
        var thisModule, txtSearchByISBN, txtSearchByTitle, txtSearchByAuthor, ddlSearchByGenre, btnSearchBooks, btnClearSearchBooks;
        var fetchCustomersTimer = null;
        return {
            init: function () {

                thisModule = this;
                txtSearchByISBN = sandbox.find("#txtSearchByISBN")[0];
                txtSearchByTitle = sandbox.find("#txtSearchByTitle")[0];
                txtSearchByAuthor = sandbox.find("#txtSearchByAuthor")[0];
                ddlSearchByGenre = sandbox.find("#ddlSearchByGenre")[0];

                btnSearchBooks = sandbox.find("#btnSearchBooks")[0];
                btnClearSearchBooks = sandbox.find("#btnClearSearchBooks")[0];

                errorModal = sandbox.find("#errorModal")[0];

                sandbox.addEvent(btnSearchBooks, "click", thisModule.findByBook);
                sandbox.addEvent(btnClearSearchBooks, "click", thisModule.clearSearchFilters);
            },
            destroy: function () {
                sandbox.removeEvent(btnSearchByName, "click", thisModule.findByBook);
                sandbox.removeEvent(btnClearSearchBooks, "click", thisModule.clearSearchFilters);
            },
            findByBook: function () {              

                if (thisModule.validateInput(txtSearchByISBN)
                    || thisModule.validateInput(txtSearchByTitle)
                    || thisModule.validateInput(txtSearchByAuthor)
                    || thisModule.validateInput(ddlSearchByGenre)) {

                    var isbn = txtSearchByISBN.value;
                    var title = txtSearchByTitle.value;
                    var author = txtSearchByAuthor.value;
                    var genre = ddlSearchByGenre.value;

                    iRequestConfig.updateISBNFilter(isbn);
                    iRequestConfig.updateTitleFilter(title);
                    iRequestConfig.updateAuthorFilter(author);
                    iRequestConfig.updateGenreFilter(genre);
                    iRequestConfig.updateOffset(0);

                    thisModule.clearValidationNotices();
                    //Reload Grid
                    sandbox.notify({
                        type: "init-book-grid"
                    });
                }
                else {
                    sandbox.notify({
                        type: "empty-book-grid"
                    });
                }
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
            clearSearchFilters: function () {
                //START HERE
                txtSearchByISBN.value = "";
                txtSearchByTitle.value = "";
                txtSearchByAuthor.value = "";
                ddlSearchByGenre.value = "";

                iRequestConfig.updateISBNFilter("");
                iRequestConfig.updateTitleFilter("");
                iRequestConfig.updateAuthorFilter("");
                iRequestConfig.updateGenreFilter("");

                thisModule.clearValidationNotices();

                sandbox.notify({
                    type: "empty-book-grid"
                });
            },
            clearValidationNotices: function () {
                sandbox.removeClass(txtSearchByISBN, "alert-danger");
                sandbox.removeClass(txtSearchByTitle, "alert-danger");
                sandbox.removeClass(txtSearchByAuthor, "alert-danger");
                sandbox.removeClass(ddlSearchByGenre, "alert-danger");
            }
        }
    }
    return bookSearchBoxModule;
});