﻿requirejs.config({
    baseUrl: "http://localhost:4561/Scripts/Base",
    paths: {
        Core: "http://localhost:4561/Scripts/Core",
        Service: "http://localhost:4561/Scripts/Service",
        Sandbox: "http://localhost:4561/Scripts/Sandbox",
        Modules: "http://localhost:4561/Scripts/Modules",
        Store: "http://localhost:4561/Scripts/Store",
        SharedModules: "http://localhost:4561/Scripts/Modules/Shared",
        Config: "http://localhost:4561/Scripts/Config",
    },
    shim: {
        bootstrap: {
            deps: ["jquery"]
        },
        datetimepicker: {
            deps: ["jquery"]
        },
        signalR: {
            deps: ['jquery']
        }
    }
});

// Start the main app logic.
requirejs(["Core/core-jquery", "Modules/book-grid", "Modules/book-searchbox", "Modules/book-lender", "SharedModules/alert-modals"],
function (icore, iBookGrid, iBookSearchbox, iBookLenderForm, iAlertModals) {

    icore.register("alert-modals", iAlertModals);
    icore.register("book-grid", iBookGrid);
    icore.register("book-searchbox", iBookSearchbox);
    icore.register("book-lenderform", iBookLenderForm)

    icore.start_all();
    //setTimeout(ICORE.stop_all(), 10000);
});

