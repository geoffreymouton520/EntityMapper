(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var addDomainCtrl = function (domain, $state, toastr) {
        var vm = this;
        vm.domain = domain;
        vm.domain.active = true;
        vm.submit = function (isValid) {
            if (isValid) {
                vm.domain.$save(function (data) {
                    vm.domain = data;
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
        vm.cancel = function() {
            $state.go("domains");
        }
    };

    mapperClient.controller("addDomainCtrl", ["domain", "$state", "toastr", addDomainCtrl]);
})();
