(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var editDomainCtrl = function (domain, $state, toastr) {
        var vm = this;
        vm.domain = domain;
        //$scope.message = "Editing Domain";

        vm.submit = function (isValid) {
            if (isValid) {
                vm.domain.$update(function (data) {
                    vm.domain = data;
                    toastr.success("Save Successfull");
                    $state.go("domains");
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
        vm.cancel = function() {
            $state.go("domains");
        };
    };


    mapperClient.controller("editDomainCtrl", ["domain", "$state", "toastr", editDomainCtrl]);
})();
