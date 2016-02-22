(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var domainsCtrl = function ($scope, domainResource, sweet) {
        var vm = this;
        vm.name = "Domains";
        vm.actions = "View, Add, Update, & Delete Domains";
        var loadDomains = function () {
            vm.loading = true;
            domainResource.query(function (data) {
                vm.domains = data;
                vm.loading = false;
            });
        };

        vm.deleteDomain = function (domain) {
            sweet.show({
                title: "Are you sure?",
                text: "You will not be able to recover this domain.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    domain.$delete(function () {
                        sweet.show("Deleted!", "Your domain has been deleted.", "success");
                        loadDomains();
                    }, function (response) {
                        var message = response.statusText + "\r\n";
                        if (response.data.modelState) {
                            for (var key in response.data.modelState) {
                                if (response.data.modelState.hasOwnProperty(key)) {
                                    message += response.data.modelState[key];
                                }
                            }
                        }

                        if (response.data.exceptionMessage) {
                            message += response.data.exceptionMessage;
                        }
                        sweet.show("Error", message, "error");
                    });

                } else {
                    sweet.show("Cancelled", "Your domain is safe", "error");
                }
            });
        };

        vm.reload = function () {
            loadDomains();
        };

        loadDomains();
    };

    mapperClient.controller("domainsCtrl", ["$scope", "domainResource", "sweet", domainsCtrl]);
})();
