(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var viewPropertyMappingCtrl = function ($scope, propertyMapping) {
        var vm = this;
        vm.propertyMapping = propertyMapping;
        vm.message = "Viewing propertyMapping";
    };

    mapperClient.controller("viewPropertyMappingCtrl", ["$scope", "propertyMapping", viewPropertyMappingCtrl]);
})();