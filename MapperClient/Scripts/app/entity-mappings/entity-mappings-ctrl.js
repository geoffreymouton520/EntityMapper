(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var entityMappingsCtrl = function ($scope, entityMappingResource) {
        var vm = this;
        vm.name = "EntityMappings";
        vm.actions = "View, Add, Update, & Delete EntityMappings";
        var loadEntityMappings = function () {
            return entityMappingResource.query(function (data) {
                vm.entityMappings = data;
            });
        };

        vm.deleteEntityMapping = function (entityMapping) {
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this entityMapping.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    entityMapping.$delete(function (data) {
                        swal("Deleted!", "Your entityMapping has been deleted.", "success");
                        loadEntityMappings();
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
                        swal("Error", message, "error");
                    });

                } else {
                    swal("Cancelled", "Your entityMapping is safe", "error");
                }
            });
        };
        $scope.myPromise = loadEntityMappings();
    };

    mapperClient.controller("entityMappingsCtrl", ["$scope", "entityMappingResource", entityMappingsCtrl]);
})();
