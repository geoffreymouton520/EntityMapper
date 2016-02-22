(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var confirmEntityMappingCtrl = function (entityMapping, $state, entityResource, toastr) {
        var vm = this;
        vm.entityMapping = entityMapping;
        vm.entityMapping.sourceEntityId += "";
        vm.entityMapping.destinationEntityId += "";

        entityResource.query(function (data) {
            vm.sourceEntities = data;
            vm.destinationEntities = data;
        });

        var successfullyUpdated = function (data) {
            vm.entityMapping = data;
            toastr.success("Save Successfull");
            $state.go("entityMappings");
        };
        var failedToUpdated = function (response) {
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
                vm.entityMapping.confirmed = true;
                vm.entityMapping.$update(successfullyUpdated, failedToUpdated);
            } else {
                toastr.error("Please correct the validation errors first.");
            }
        };
        vm.cancel = function () {
            $state.go("entityMappings");
        }
    };


    mapperClient.controller("confirmEntityMappingCtrl", ["entityMapping", "$state", "entityResource", "toastr", confirmEntityMappingCtrl]);
})();
