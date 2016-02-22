(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var addMappingOriginCtrl = function (mappingOrigin, $state, domainResource) {
        var vm = this;
        vm.mappingOrigin = mappingOrigin;
        vm.mappingOrigin.active = true;
        vm.mappingOrigin.domainId = "";
        domainResource.query(function (data) {
            vm.domains = data;
        });
        
        vm.submit = function (isValid) {
            if (isValid) {
                vm.mappingOrigin.$save(function (data) {
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
        }
        vm.toInt = function(value) {
            return parseInt(value);
        };
    };

    mapperClient.controller("addMappingOriginCtrl", ["mappingOrigin", "$state","domainResource", addMappingOriginCtrl]);
})();
