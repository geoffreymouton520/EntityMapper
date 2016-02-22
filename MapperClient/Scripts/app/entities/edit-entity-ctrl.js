(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var editEntityCtrl = function (entity, $state, systemResource, toastr) {
        var vm = this;
        vm.entity = entity;
        vm.entity.systemId = vm.entity.systemId + "";
        //vm.entity.systemId =  "1";
        systemResource.query(function (data) {
            vm.systems = data;
        });
        //$scope.message = "Editing Entity";

        vm.submit = function (isValid) {
            if (isValid) {
                vm.entity.$update(function (data) {
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
        };
    };


    mapperClient.controller("editEntityCtrl", ["entity", "$state", "systemResource","toastr", editEntityCtrl]);
})();
