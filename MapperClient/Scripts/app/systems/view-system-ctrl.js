(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var viewSystemCtrl = function ($scope, system) {
        var vm = this;
        vm.system = system;
        vm.message = "Viewing system";
    };

    mapperClient.controller("viewSystemCtrl", ["$scope", "system", viewSystemCtrl]);
})();