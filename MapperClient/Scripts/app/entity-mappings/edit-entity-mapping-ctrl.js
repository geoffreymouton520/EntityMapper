(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var editEntityMappingCtrl = function (entityMapping, $state, domainResource, systemResource, entityResource) {
        var vm = this;
        vm.entityMapping = entityMapping;
        vm.entityMapping.domainId += "";
        vm.entityMapping.sourceSystemId += "";
        vm.entityMapping.destinationSystemId += "";
        vm.entityMapping.sourceEntityId += "";
        vm.entityMapping.destinationEntityId += "";
        domainResource.query(function (data) {
            vm.domains = data;
        });
        systemResource.query(function (data) {
            vm.systems = data;
        });

        entityResource.query(function (data) {
            vm.sourceEntities = data;
            vm.destinationEntities = data;
        });


        vm.domainChanged = function () {
            systemResource.getByDomain({ domainId: vm.entityMapping.domainId }, function (data) {
                vm.systems = data;
                // console.log(vm.systems);
            });

        };

        vm.destinationSystemChanged = function () {
            entityResource.getBySystem({ systemId: vm.entityMapping.destinationSystemId }, function (data) {
                vm.destinationEntities = data;
            });
        };
        vm.sourceSystemChanged = function () {
            entityResource.getBySystem({ systemId: vm.entityMapping.sourceSystemId }, function (data) {
                vm.sourceEntities = data;
            });
        };
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
                vm.entityMapping.$update(successfullyUpdated, failedToUpdated);
            } else {
                toastr.error("Please correct the validation errors first.");
            }
        };
        vm.cancel = function () {
            $state.go("entityMappings");
        }
    };


    mapperClient.controller("editEntityMappingCtrl", ["entityMapping", "$state", "domainResource", "systemResource", "entityResource", editEntityMappingCtrl]);
})();
