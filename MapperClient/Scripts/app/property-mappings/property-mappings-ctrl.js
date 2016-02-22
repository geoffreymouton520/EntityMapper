(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var propertyMappingsCtrl = function ($scope, propertyMappingResource) {
        var vm = this;
        vm.name = "PropertyMappings";
        vm.actions = "View, Add, Update, & Delete PropertyMappings";
        var loadPropertyMappings = function () {
            propertyMappingResource.query(function (data) {
                vm.propertyMappings = data;
            });
        };

        vm.deletePropertyMapping = function (propertyMapping) {
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this propertyMapping.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    propertyMapping.$delete(function (data) {
                        swal("Deleted!", "Your propertyMapping has been deleted.", "success");
                        loadPropertyMappings();
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
                    swal("Cancelled", "Your propertyMapping is safe", "error");
                }
            });
        };
        loadPropertyMappings();
    };

    mapperClient.controller("propertyMappingsCtrl", ["$scope", "propertyMappingResource", propertyMappingsCtrl]);
})();
