(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var automatedEntityMappingCtrl = function ($state, domainResource, systemResource, entityMappingResource, toastr) {
        var vm = this;
        var load;
        vm.session = {};
        domainResource.query(function (data) {
            vm.domains = data;
        });
        systemResource.query(function (data) {
            vm.systems = data;
        });


        vm.domainChanged = function () {
            systemResource.getByDomain({ domainId: vm.session.domainId }, function (data) {
                vm.systems = data;
            });

        };

        var successfullyAutomated = function (data) {
            if (load) {
                load.remove();
            }
            vm.entityMapping = data;
            toastr.success("Save Successfull");
            $state.go("entityMappings");
        };
        var failedToAutomate = function (response) {
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
        };
        vm.submit = function (isValid) {
            if (isValid) {
                load = toastr.success("Loading..", { extendedTimeOut: 0 });
                entityMappingResource.automate(vm.session, successfullyAutomated, failedToAutomate);
            } else {
                toastr.error("Please correct the validation errors first.");
            }
        };
        vm.cancel = function () {
            $state.go("entityMappings");
        }
    };


    mapperClient.controller("automatedEntityMappingCtrl", ["$state", "domainResource", "systemResource", "entityMappingResource", "toastr", automatedEntityMappingCtrl]);
})();
