(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var systemsCtrl = function ($scope, systemResource, sweet) {
        var vm = this;
        vm.name = "Systems";
        vm.actions = "View, Add, Update, & Delete Systems";
        var loadSystems = function () {
            systemResource.query(function (data) {
                vm.systems = data;
            });
        };

        vm.deleteSystem = function (system) {
            sweet.show({
                title: "Are you sure?",
                text: "You will not be able to recover this system.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    system.$delete(function () {
                        sweet.show("Deleted!", "Your system has been deleted.", "success");
                        loadSystems();
                    }, function (response) {
                        var message = response.statusText + "\r\n";
                        if (response.data.modelState) {
                            for (var key in response.data.modelState) {
                                if (response.data.modelState.hasOwnProperty(key)) {
                                    message +=  response.data.modelState[key];
                                }
                            }
                        }

                        if (response.data.exceptionMessage) {
                            message += response.data.exceptionMessage;
                        }
                        sweet.show("Error", message, "error");
                    });

                } else {
                    sweet.show("Cancelled", "Your system is safe", "error");
                }
            });
        };
        vm.reload = function() {
             loadSystems();
        };
        loadSystems();
    };

    mapperClient.controller("systemsCtrl", ["$scope", "systemResource", "sweet", systemsCtrl]);
})();
