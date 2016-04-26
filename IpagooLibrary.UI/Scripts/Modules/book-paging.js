define(["Config/people-RequestConfig"], function (iRequestConfig) {
    var peoplePagingModule = function (sandbox) {
        var pagingContainer, pagerTemplate, thisModule, peopleItemsDisplayedLimit, peopleNumberOfPageButtonsDisplayed, lnkPeoplePages, loadLastPagerButtons, loadPager = "true";

        return {
            init: function () {
                thisModule = this;
                pagingContainer = sandbox.find("#people-pager-container")[0];
                pagerTemplate = sandbox.find("#people-pager-template")[0];
                sandbox.listen({
                    "load-pager": thisModule.initializePager,
                    "set-mustloadpager-status": thisModule.SetMustLoadPagerStatus,
                    "empty-people-paging": thisModule.emptyPeoplePaging
                });

            },
            destroy: function () {
                sandbox.ignore(["load-pager", "empty-people-paging"]);
            },
            emptyPeoplePaging: function () {
                sandbox.replaceContent(pagingContainer, "");
            },
            initializePager: function (data) {
                
                if (data) {
                    //Check whether the paginator needs loading, as we might not be on the next batch of paging options
                    if (loadPager === "true") {

                        var offset = data.Offset;
                        var totalPeople = data.TotalPeople;
                        var totalPages = data.TotalPages;
                        var peopleDisplayedPerPage = iRequestConfig.getPeopleDisplayedPerPage();
                        var numberOfPagerButtonDisplayed = iRequestConfig.getNumberOfPagerButtonDisplayed();

                        var currentPageIndex = loadLastPagerButtons !== "true" ? parseInt((offset / peopleDisplayedPerPage) + 1) : (totalPages - numberOfPagerButtonDisplayed) + 1;

                        currentPageIndex = currentPageIndex < 0 ? 1 : currentPageIndex;

                        var pageButtonOptionsToRight = totalPeople > peopleDisplayedPerPage ? thisModule.CalculateNumberOfPageButtonsToRight(currentPageIndex, numberOfPagerButtonDisplayed, totalPages) : 0;

                        //Set's the offset for the "Last" button on the pager
                        var lastButtonOffset = totalPeople > peopleDisplayedPerPage? totalPeople - (peopleDisplayedPerPage * numberOfPagerButtonDisplayed): 0;

                        var pageNumbers = thisModule.loadNextPageNumbersIntoArray(currentPageIndex, offset, pageButtonOptionsToRight, peopleDisplayedPerPage, totalPages);

                        var pager = {
                            PageNumbers: pageNumbers,
                            LastOffset: lastButtonOffset
                        }

                        var template = sandbox.getTemplate(pagerTemplate);

                        var generatedHtml = template(pager);
                        sandbox.replaceContent(pagingContainer, generatedHtml);

                        thisModule.initializeClickEvents();
                    }
                }
                else {
                    sandbox.replaceContent(pagingContainer, "");
                }
            },
            CalculateNumberOfPageButtonsToRight: function (currentPageIndex, numberOfPagerButtonDisplayed, totalPages) {
                
                var count = 1;
                //Max number of paging buttons to add to the right of the current selected pager button is always 1 less than the full amount the pager can contain
                var maxPagingButtonOneCanAdd = numberOfPagerButtonDisplayed - 1;

                //Checks whether we can add another full tray of paging buttons
                if ((currentPageIndex + maxPagingButtonOneCanAdd) <= totalPages) {
                    maxPagingButtonOneCanAdd = numberOfPagerButtonDisplayed - 1;
                } else {
                    maxPagingButtonOneCanAdd = 0;
                    //Keeps incementing the amount of buttons one can add till the max number of page buttons allowed is reached
                    while ((currentPageIndex + count) <= totalPages) {
                        maxPagingButtonOneCanAdd = count;
                        count++;
                    }
                }
                return maxPagingButtonOneCanAdd;
            },
            loadNextPageNumbersIntoArray: function (currentPageIndex, peopleOffset, pageButtonOptionsToRight, peopleDisplayedPerPage, totalPages) {
                
                var count = 1;
                var numberToPush = 0;
                var pageNumbers = [];
                var currentOffset = 0;
                var isLastButtonInPager = false;

                //Add the ACTIVE page 
                pageNumbers.push({ PageNum: currentPageIndex, IsCurrentPage: true, Offset: peopleOffset, IsLastButtonInPager: isLastButtonInPager });

                //Add any paging buttons AFTER the active page 
                for (; count <= pageButtonOptionsToRight; count++) {
                    numberToPush = currentPageIndex + count;

                    currentOffset = peopleOffset + (peopleDisplayedPerPage * count);

                    isLastButtonInPager = count === pageButtonOptionsToRight && (currentPageIndex + count) !== totalPages;

                    pageNumbers.push({ PageNum: numberToPush, IsCurrentPage: false, Offset: currentOffset, IsLastButtonInPager: isLastButtonInPager });
                }

                return pageNumbers;
            },
            initializeClickEvents: function () {

                lnkPeoplePages = sandbox.find(".lnkPeoplePage");

                var count = 0;
                for (; count < lnkPeoplePages.length; count++) {

                    var lnkpeoplePage = lnkPeoplePages[count];
                    sandbox.addEvent(lnkpeoplePage, "click", thisModule.PagePeople);
                }
            },
            destroyClickEvents: function () {

                if (lnkPeoplePages && lnkPeoplePages.length > 0) {
                    var count = 0;
                    for (; count < lnkPeoplePages.length; count++) {

                        var lnkPeoplePage = lnkPeoplePages[count];
                        sandbox.addEvent(lnkPeoplePage, "click", thisModule.PagePeople);
                    }
                }
            },
            PagePeople: function (e) {
                
                var currentClickedPageLink = e.currentTarget;
                var pageOffset = sandbox.getAttr(currentClickedPageLink, "data-offset");
                loadPager = sandbox.getAttr(currentClickedPageLink, "data-reloadpager");
                loadLastPagerButtons = sandbox.getAttr(currentClickedPageLink, "data-loadlastpagerbutton");

                iRequestConfig.updateOffset(pageOffset);

                //Reload Grid
                sandbox.notify({
                    type: "init-people-grid"
                });

                thisModule.SetActivePageSelected(currentClickedPageLink);
            },
            SetActivePageSelected: function (activePageButton) {
                lnkPeoplePages = sandbox.find(".lnkPeoplePage");

                var count = 0;
                for (; count < lnkPeoplePages.length; count++) {

                    var lnkPeoplePage = lnkPeoplePages[count].parentElement;
                    sandbox.removeClass(lnkPeoplePage, "active");
                }

                sandbox.addClass(activePageButton.parentElement, "active");
            },
            SetMustLoadPagerStatus: function (data) {
                loadPager = data;
            }
        }
    }
    return peoplePagingModule;
});