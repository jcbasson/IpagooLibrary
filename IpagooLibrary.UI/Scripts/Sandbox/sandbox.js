
define(
function () {
    var sandbox = {
        
        create: function (core, moduleSelector) {
            var container = core.dom.query("#" + moduleSelector);
            return {
                find: function (selector) {
                    
                    return container.query(selector);
                },
                addEvent: function (element, event, fn) {
                    core.dom.bind(element, event, fn);
                },
                removeEvent: function (element, event, fn) {
                    core.dom.unbind(element, event, fn);
                },
                notify: function (event) {

                    if (core.is_obj(event) && event.type) {
                        core.triggerEvent(event);
                    }
                },
                listen: function (events) {

                    if (core.is_obj(events)) {
                        core.registerEvents(events, moduleSelector);
                    }
                },
                ignore: function (events) {
                    if (core.is_arr(events)) {
                        core.removeEvents(events, moduleSelector);
                    }
                },
                create_element: function (el, config) {
                    var i, text;
                    el = core.dom.create(el);
                    if (config) {
                        if (config.children && core.is_arr(config.children)) {
                            i = 0;
                            while (config.children[i]) {
                                el.appendChild(config.children[i]);
                                i++;
                            }
                            delete config.children;
                        } else if (config.text) {
                            text = document.createTextNode(config.text);
                            delete config.text;
                            el.appendChild(text);
                        }
                        core.dom.apply_attrs(el, config);
                    }
                    return el;
                },
                remove_element: function(el) {
                    core.dom.remove(el);
                },
                appendContent: function(el, content) {
                    core.dom.appendContent(el, content);
                },
                replaceContent: function (el, content) {
                    core.dom.replaceContent(el, content);
                },
                addCss: function(el, css) {
                    core.dom.addCss(el, css);
                },
                addClass: function (el, classs) {
                    core.dom.addClass(el, classs);
                },
                removeClass: function (el, classs) {
                    core.dom.removeClass(el, classs);
                },
                toggleAllCheckboxesByClass: function (className, checked) {
                    core.dom.toggleAllCheckboxesByClass(className, checked);
                },
                addAttr: function (el, attrs) {
                    core.dom.apply_attrs(el, attrs);
                },
                getAttr: function (el, attr) {
                   return core.dom.get_attr(el, attr);
                },
                removeAttr: function(el, attr){
                    core.dom.remove_attr(el, attr);
                },
                addOption: function (el, valueToAdd, textToAdd) {
                    core.dom.addOption(el, valueToAdd, textToAdd);
                },
                removeOption: function (el, valueToRemove) {
                    core.dom.removeOption(el, valueToRemove);
                },
                clearOptions: function (el) {
                    core.dom.empty(el);
                },
                httpGet: function (url) {
                    return core.httpService.httpGet(url);
                },
                httpPost:function(url, data) {
                    return core.httpService.httpPost(url, data);
                },
                getTemplate: function (el) {
                    return core.dom.getTemplate(el);
                },
                showModal: function(el, message) {
                    core.dom.showModal(el, message);
                },
                hideModal: function(el) {
                    core.dom.hideModal(el);
                },
                initializeDatePicker: function (el) {
                    return core.dom.initializeDatePicker(el);
                }
            };
        }
    }
    return sandbox;
});