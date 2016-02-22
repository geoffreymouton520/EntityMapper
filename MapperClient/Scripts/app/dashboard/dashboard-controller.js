(function() {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var dashboardCtrl = function($scope) {

        // create a message to display in our view
        $scope.name = "Dashboard";
        $scope.actions = "View Mapping Statistics";
        $scope.message = "Everyone come and see how good I look!";
    };

    mapperClient.controller("dashboardCtrl", dashboardCtrl);
})();

