(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var viewDomainCtrl = function ($scope, domain) {
        var vm = this;
        vm.domain = domain;
        vm.message = "Viewing domain";
    };

    mapperClient.controller("viewDomainCtrl", ["$scope", "domain", viewDomainCtrl]);
})();