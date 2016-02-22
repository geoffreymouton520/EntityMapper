(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var editPropertyMappingCtrl = function (propertyMapping, $state) {
        var vm = this;
        vm.propertyMapping = propertyMapping;
        //$scope.message = "Editing PropertyMapping";

        vm.submit = function (isValid) {
            if (isValid) {
                vm.propertyMapping.$update(function (data) {
                    vm.propertyMapping = data;
                    toastr.success("Save Successfull");
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

                    toastr.error(message);
                });
            } else {
                toastr.error("Please correct the validation errors first.");
            }
        };
        vm.cancel = function () {
            $state.go("propertyMappings");
        };
    };


    mapperClient.controller("editPropertyMappingCtrl", ["propertyMapping", "$state", editPropertyMappingCtrl]);
})();
