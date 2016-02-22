(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var addPropertyCtrl = function (property, $state, entityResource) {
        var vm = this;
        vm.property = property;
        vm.property.active = true;
        vm.property.entityId = "";
        entityResource.query(function (data) {
            vm.entities = data;
        });
        
        vm.submit = function (isValid) {
            if (isValid) {
                vm.property.$save(function (data) {
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
        }
        vm.toInt = function(value) {
            return parseInt(value);
        };
    };

    mapperClient.controller("addPropertyCtrl", ["property", "$state","entityResource", addPropertyCtrl]);
})();
