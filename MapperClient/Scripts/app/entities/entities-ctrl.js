(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var entitiesCtrl = function ($scope, entityResource) {
        var vm = this;
        vm.name = "Entities";
        vm.actions = "View, Add, Update, & Delete Entities";
        var loadEntities = function () {
            entityResource.query(function (data) {
                vm.entities = data;
            });
        };

        vm.deleteEntity = function (entity) {
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this entity.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    entity.$delete(function (data) {
                        swal("Deleted!", "Your entity has been deleted.", "success");
                        loadEntities();
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
                        swal("Error", message, "error");
                    });

                } else {
                    swal("Cancelled", "Your entity is safe", "error");
                }
            });
        };

        vm.reload = function() {
            loadEntities();
        };
        loadEntities();
    };

    mapperClient.controller("entitiesCtrl", ["$scope", "entityResource", entitiesCtrl]);
})();
