(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var addEntityCtrl = function (entity, $state, systemResource, toastr) {
        var vm = this;
        vm.entity = entity;
        vm.entity.active = true;
        vm.entity.systemId = "";
        systemResource.query(function (data) {
            vm.systems = data;
        });
        
        vm.submit = function (isValid) {
            if (isValid) {
                vm.entity.$save(function (data) {
                    vm.entity = data;
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
            $state.go("entities");
        }
        vm.toInt = function(value) {
            return parseInt(value);
        };
    };

    mapperClient.controller("addEntityCtrl", ["entity", "$state","systemResource","toastr", addEntityCtrl]);
})();
