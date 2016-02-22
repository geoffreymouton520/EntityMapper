(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var viewEntityCtrl = function ($scope, entity) {
        var vm = this;
        vm.entity = entity;
        vm.message = "Viewing entity";
    };

    mapperClient.controller("viewEntityCtrl", ["$scope", "entity", viewEntityCtrl]);
})();