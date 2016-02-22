(function () {
    "use strict";
    var common = angular.module("common.services");

    var currentUser = function () {
        var profile = {
            isLoggedIn: false,
            userName: "",
            token: ""
        };
        var setProfile = function (userName, token) {
            profile.userName = userName;
            profile.token = token;
            profile.isLoggedIn = true;
        };
        var getProfile = function () {
            return profile;
        };
        return {
            setProfile: setProfile,
            getProfile: getProfile
        };
    };

    common.factory("currentUser", currentUser);
})();