(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var editPropertyCtrl = function (property, $state) {
        var vm = this;
        vm.property = property;
        //$scope.message = "Editing Property";

        vm.submit = function (isValid) {
            if (isValid) {
                vm.property.$update(function (data) {
                    vm.property = data;
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
            $state.go("properties");
        };
    };


    mapperClient.controller("editPropertyCtrl", ["property", "$state", editPropertyCtrl]);
})();
