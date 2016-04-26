define(["jquery", "handlebarsBase", "Core/core-config", "Service/ajax-service", "Service/error-service", "Sandbox/sandbox", "bootstrap", "bootstrap-datepicker", "signalR"],
function (iJquery, iHandlebars, iCoreConfig, iAjaxService, iErrorService, iSandbox) {

    var moduleData = {}, appConfig = {}, filterUtitlity = {}, signalROjbect = { connection: '', hubProxy : ''};

    return {
        start_all: function () {


            if (iJquery && iHandlebars && iCoreConfig && iAjaxService && iSandbox) {

                signalROjbect.connection = iJquery.hubConnection();

                if (signalROjbect.connection) {

                    signalROjbect.hubProxy = signalROjbect.connection.createHubProxy('LibraryHub');

                    if (signalROjbect.hubProxy) {

                        appConfig = iCoreConfig();
                                                
                        var thisCore = this;

                        for (var moduleId in moduleData) {

                            if (moduleData.hasOwnProperty(moduleId)) {

                                thisCore.start(moduleId);
                            }
                        }
                    }
                    else {
                        throw "Unable to generate library hub proxies";
                    }
                }
                else {
                    throw "Unable to make a connection to library hub";
                }

            } else {
                throw "One or more Base Libraries are missing";
            }
        },
        start: function (moduleId) {

            var mod = moduleData[moduleId];
            if (mod) {
                mod.instance = mod.create(iSandbox.create(this, moduleId), signalROjbect);
                mod.instance.init();
            }
        },
        stop: function (moduleId) {

            var data = moduleData[moduleId];

            if (data && data.instance) {
                data.instance.destroy();
                data.instance = null;
            } else {
                throw "Stop Module '" + moduleId + "': FAILED : module does not exist or has not been started";
            }
        },
        stop_all: function () {

            for (var moduleId in moduleData) {
                if (moduleData.hasOwnProperty(moduleId)) {
                    this.stop(moduleId);
                }
            }
        },
        register: function (moduleId, creator) {

            var temp;
            if (typeof moduleId === "string" && typeof creator === "function") {
                temp = creator(iSandbox.create(this, moduleId));
                if (temp.init && temp.destroy && typeof temp.init === "function" && typeof temp.destroy === "function") {
                    moduleData[moduleId] = {
                        create: creator,
                        instance: null
                    };
                    temp = null;
                } else {
                    throw "Module \"" + moduleId + "\" Registration: FAILED: instance has no init or destroy functions";
                }
            } else {
                throw "Module \"" + moduleId + "\" Registration: FAILED: one or more arguments are of incorrect type";
            }
        },
        registerEvents: function (evts, mod) {

            if (this.is_obj(evts) && mod) {
                if (moduleData[mod]) {
                    moduleData[mod].events = evts;
                }
            }
        },
        triggerEvent: function (evt) {

            var mod;
            for (mod in moduleData) {
                if (moduleData.hasOwnProperty(mod)) {

                    mod = moduleData[mod];
                    if (mod.events && mod.events[evt.type]) {

                        mod.events[evt.type](evt.data);
                    }
                }
            }
        },
        removeEvents: function (evts, mod) {
            var i = 0, evt;
            if (this.is_arr(evts) && mod && (mod = moduleData[mod]) && mod.events) {
                for (; evt === evts[i++];) {
                    delete mod.events[evt];
                }
            }
        },
        httpService: {
            httpGet: function (url) {
                var apiBaseUrl = appConfig.getApiBaseUrl();
                url = apiBaseUrl + url;
                return iAjaxService.HttpGet(url);
            },
            httpPost: function (url, data) {

                var apiBaseUrl = appConfig.getApiBaseUrl();
                url = apiBaseUrl + url;
                return iAjaxService.HttpPost(url, data);
            }
        },
        dom: {
            query: function (selector, context) {

                var ret = {}, that = this, jqEls, i = 0;

                if (context && context.find) {
                    jqEls = context.find(selector);
                } else {
                    jqEls = iJquery(selector);
                }

                ret = jqEls.get();
                ret.length = jqEls.length;
                ret.query = function (sel) {
                    return that.query(sel, jqEls);
                }
                return ret;
            },
            bind: function (element, evt, fn) {

                if (element && evt) {
                    if (typeof evt === "function") {
                        fn = evt;
                        evt = "click";
                    }
                    iJquery(element).bind(evt, fn);
                } else {
                    throw "Wrong Arguments";
                }
            },
            unbind: function (element, evt, fn) {

                if (element && evt) {
                    if (typeof evt === "function") {
                        fn = evt;
                        evt = "click";
                    }
                    iJquery(element).unbind(evt, fn);
                } else {
                    throw "Wrong Arguments";
                }
            },
            create: function (el) {
                return document.createElement(el);
            },
            remove: function (el) {
                iJquery(el).remove();
            },
            appendContent: function (el, content) {
                iJquery(el).append(content);
            },
            replaceContent: function (el, content) {
                iJquery(el).html(content);
            },
            apply_attrs: function (el, attrs) {
                iJquery(el).attr(attrs);
            },
            get_attr: function (el, attribute) {
                return iJquery(el).attr(attribute);
            },
            remove_attr: function (el, attribute) {
                iJquery(el).removeAttr(attribute);
            },
            addCss: function (el, css) {
                iJquery(el).css(css.Property, css.Value);
            },
            addClass: function (el, classs) {
                iJquery(el).addClass(classs);
            },
            removeClass: function (el, classs) {
                iJquery(el).removeClass(classs);
            },
            toggleAllCheckboxesByClass: function (className, checked) {
                var checkboxes = document.getElementsByClassName(className);
                var checkbox, count;
                for (count = 0; count < checkboxes.length; count++) {

                    checkbox = checkboxes[count];
                    checkbox.checked = checked;

                }
            },
            addOption: function (el, valueToAdd, textToAdd) {
                iJquery(el).append(iJquery("<option>").attr("value", valueToAdd).text(textToAdd));
            },
            removeOption: function (el, valueToRemove) {
                iJquery(el).children("option[value=" + valueToRemove + "]").remove();
            },
            empty: function (el) {
                iJquery(el).empty();
            },
            getTemplate: function (el) {
                var sourceHtml = iJquery(el).html();
                return iHandlebars.compile(sourceHtml);
            },
            showModal: function (el, message) {

                if (message) {
                    var msgEl = iJquery(el).find(".alert-message");
                    msgEl.html(message);
                }
                iJquery(el).modal();
            },
            hideModal: function (el) {
                iJquery(el).modal("hide");
            },
            addAnimationEvents: function (el, evts, func) {
                iJquery(el).modal();
            },
            initializeDatePicker: function (el) {

                var context = this;
                context.datepickers = [];
                iJquery(el).each(function () {
                    var datepicker = iJquery(this).datepicker({
                        format: "dd/mm/yyyy",
                        startDate: "-0d",
                        showAnim: "fold",
                        autoSize: true,
                        autoclose: true,
                        todayHighlight: true
                    })

                    context.datepickers.push(datepicker);

                });
            },
        },
        is_arr: function (arr) {
            return iJquery.isArray(arr);
        },
        is_obj: function (obj) {
            return iJquery.isPlainObject(obj);
        }
    };
});