(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var viewMappingOriginCtrl = function ($scope, mappingOrigin) {
        var vm = this;
        vm.mappingOrigin = mappingOrigin;
        vm.message = "Viewing mappingOrigin";
    };

    mapperClient.controller("viewMappingOriginCtrl", ["$scope", "mappingOrigin", viewMappingOriginCtrl]);
})();