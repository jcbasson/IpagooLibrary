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

                setTimeout(function () { sandbox.hideModal(successModal); }, 2000);
            },
            alertInfo: function (message) {
                sandbox.showModal(infoModal, message);

                setTimeout(function () { sandbox.hideModal(infoModal); }, 2000);
            },
            alertWarning: function (message) {
                sandbox.showModal(warningModal, message);

                setTimeout(function () { sandbox.hideModal(warningModal); }, 2000);
            },
            alertDanger: function (message) {
                sandbox.showModal(dangerModal, message);

                setTimeout(function () { sandbox.hideModal(dangerModal); }, 2000);
            }
        }
    }
    return alertModalsModule;
});