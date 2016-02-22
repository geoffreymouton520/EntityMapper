(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var mappingOriginsCtrl = function ($scope, mappingOriginResource) {
        var vm = this;
        vm.name = "MappingOrigins";
        vm.actions = "View, Add, Update, & Delete MappingOrigins";
        var loadMappingOrigins = function () {
            mappingOriginResource.query(function (data) {
                vm.mappingOrigins = data;
            });
        };

        vm.deleteMappingOrigin = function (mappingOrigin) {
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this mappingOrigin.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    mappingOrigin.$delete(function (data) {
                        swal("Deleted!", "Your mappingOrigin has been deleted.", "success");
                        loadMappingOrigins();
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
                    swal("Cancelled", "Your mappingOrigin is safe", "error");
                }
            });
        };
        loadMappingOrigins();
    };

    mapperClient.controller("mappingOriginsCtrl", ["$scope", "mappingOriginResource", mappingOriginsCtrl]);
})();
