(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var addPropertyMappingCtrl = function (propertyMapping, $state, domainResource) {
        var vm = this;
        vm.propertyMapping = propertyMapping;
        vm.propertyMapping.active = true;
        vm.propertyMapping.domainId = "";
        domainResource.query(function (data) {
            vm.domains = data;
        });
        
        vm.submit = function (isValid) {
            if (isValid) {
                vm.propertyMapping.$save(function (data) {
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
        }
        vm.toInt = function(value) {
            return parseInt(value);
        };
    };

    mapperClient.controller("addPropertyMappingCtrl", ["propertyMapping", "$state","domainResource", addPropertyMappingCtrl]);
})();
