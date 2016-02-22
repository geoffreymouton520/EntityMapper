(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var editSystemCtrl = function (system, $state,domainResource, toastr) {
        var vm = this;
        vm.system = system;
        vm.system.domainId = vm.system.domainId + "";
        domainResource.query(function (data) {
            vm.domains = data;
        });

        vm.submit = function (isValid) {
            if (isValid) {
                vm.system.$update(function (data) {
                    vm.system = data;
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
            $state.go("systems");
        };
    };


    mapperClient.controller("editSystemCtrl", ["system", "$state", "domainResource", "toastr", editSystemCtrl]);
})();
