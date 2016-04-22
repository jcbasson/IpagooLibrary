define(function () {
    var bookLenderModule = function(sandbox) {
        var thisModule, bookLenderFormModal;

        return {
            init: function () {
                thisModule = this;

                bookLenderFormModal = sandbox.find("#book-lenderform-modal")[0];
                               
                sandbox.listen({
                    'init-lendbook-form': thisModule.initializeLendBookForm,
                    'empty-lendbook-form': thisModule.emptyLendBookForm
                });
            },
            destroy: function () {
                sandbox.ignore(["init-lendbook-form", "empty-lendbook-form"]);
            },
            initializeLendBookForm: function (data) {

                if (data) {

                    alert("Data - ISBN : " + data.ISBN + "  LenderID : " + data.LenderID);

                    sandbox.showModal(bookLenderFormModal);
                }
            },
            emptyLendBookForm: function () {
            }
        }
    }
    return bookLenderModule;
})