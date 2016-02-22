(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var editMappingOriginCtrl = function (mappingOrigin, $state) {
        var vm = this;
        vm.mappingOrigin = mappingOrigin;
        //$scope.message = "Editing MappingOrigin";

        vm.submit = function (isValid) {
            if (isValid) {
                vm.mappingOrigin.$update(function (data) {
                    vm.mappingOrigin = data;
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
            $state.go("mappingOrigins");
        };
    };


    mapperClient.controller("editMappingOriginCtrl", ["mappingOrigin", "$state", editMappingOriginCtrl]);
})();
