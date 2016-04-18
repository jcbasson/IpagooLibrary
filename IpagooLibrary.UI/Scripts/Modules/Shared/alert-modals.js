define(function () {
    var alertModalsModule = function (sandbox) {
        var thisModule,successModal, infoModal, warningModal, dangerModal;
        return {
            init: function () {
                thisModule = this;
                successModal = sandbox.find("#successModal")[0];
                infoModal = sandbox.find("#infoModal")[0];
                warningModal = sandbox.find("#warningModal")[0];
                dangerModal = sandbox.find("#dangerModal")[0];

                sandbox.listen({
                    "alert-success": thisModule.alertSuccess,
                    "alert-info": thisModule.alertInfo,
                    "alert-warning": thisModule.alertWarning,
                    "alert-danger": thisModule.alertDanger
                });
            },
            destroy: function () {
                sandbox.ignore(["alert-success", "alert-info", "alert-warning", "alert-danger"]);
            },
            alertSuccess: function (message) {
                
                sandbox.showModal(successModal, message);
            },
            alertInfo: function (message) {
                sandbox.showModal(infoModal, message);
            },
            alertWarning: function (message) {
                sandbox.showModal(warningModal, message);
            },
            alertDanger: function (message) {
                sandbox.showModal(dangerModal, message);
            }
        }
    }
    return alertModalsModule;
});