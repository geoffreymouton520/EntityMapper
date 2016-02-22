(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var viewEntityMappingCtrl = function ($scope, entityMapping) {
        var vm = this;
        vm.entityMapping = entityMapping;
        vm.message = "Viewing entityMapping";
    };

    mapperClient.controller("viewEntityMappingCtrl", ["$scope", "entityMapping", viewEntityMappingCtrl]);
})();