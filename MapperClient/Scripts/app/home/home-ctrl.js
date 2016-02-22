(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var homeCtrl = function ($scope, currentUser) {

        // create a message to display in our view
        $scope.username = currentUser.getProfile().userName;
        $scope.actions = "View Mapping Statistics";
        $scope.message = "Everyone come and see how good I look!";
    };

    mapperClient.controller("homeCtrl", ["$scope", "currentUser", homeCtrl]);
})();

