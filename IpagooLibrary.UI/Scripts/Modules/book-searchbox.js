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

                var isbn = txtSearchByISBN.value;
                var title = txtSearchByTitle.value;
                var author = txtSearchByAuthor.value;
                var genre = ddlSearchByGenre.value;

                iRequestConfig.updateISBNFilter(isbn);
                iRequestConfig.updateTitleFilter(title);
                iRequestConfig.updateAuthorFilter(author);
                iRequestConfig.updateGenreFilter(genre);
                iRequestConfig.updateOffset(0);

                //Reload Grid
                sandbox.notify({
                    type: "init-book-grid"
                });

                //sandbox.notify({
                //    type: "set-mustloadpager-status",
                //    data: "true"
                //})

              
            },
            validateInput: function (value, inputControl) {
                if (!value) {
                    sandbox.addClass(inputControl, "alert-danger");
                    return false;
                } else {
                    sandbox.removeClass(inputControl, "alert-danger");
                    return true;
                }
            },
            clearSearchFilters: function () {

                txtSearchByISBN.value = "";
                txtSearchByTitle.value = "";
                txtSearchByAuthor.value = "";
                ddlSearchByGenre.value = "";

                iRequestConfig.updateISBNFilter("");
                iRequestConfig.updateTitleFilter("");
                iRequestConfig.updateAuthorFilter("");
                iRequestConfig.updateGenreFilter("");

                sandbox.notify({
                    type: "init-people-grid"
                });
            }
        }
    }
    return bookSearchBoxModule;
});