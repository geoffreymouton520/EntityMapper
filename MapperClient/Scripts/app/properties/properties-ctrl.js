(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var propertiesCtrl = function ($scope, propertyResource) {
        var vm = this;
        vm.name = "Properties";
        vm.actions = "View, Add, Update, & Delete Properties";
        var loadProperties = function () {
            propertyResource.query(function (data) {
                vm.propertys = data;
            });
        };

        vm.deleteProperty = function (property) {
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this property.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    property.$delete(function (data) {
                        swal("Deleted!", "Your property has been deleted.", "success");
                        loadProperties();
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
                    swal("Cancelled", "Your property is safe", "error");
                }
            });
        };
        loadProperties();
    };

    mapperClient.controller("propertiesCtrl", ["$scope", "propertyResource", propertiesCtrl]);
})();
