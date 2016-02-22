(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var viewPropertyCtrl = function ($scope, property) {
        var vm = this;
        vm.property = property;
        vm.message = "Viewing property";
    };

    mapperClient.controller("viewPropertyCtrl", ["$scope", "property", viewPropertyCtrl]);
})();