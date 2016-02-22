(function () {
    "use strict";
    var common = angular.module("common.services");

    var userAccount = function (appHost,$resource) {
        return {
            registration: $resource(appHost + "/api/account/register", null, {
                registerUser: {
                    method: "POST"
                }
            }),
            login: $resource(appHost + "/token", null, {
                loginUser: {
                    method: "POST",
                    headers: { "Content-Type": "application/x-www-form-urlencoded" },
                    transformRequest:function(data, headersGetter) {
                        var queryString = [];
                        for (var key in data) {
                            if (data.hasOwnProperty(key)) {
                                queryString.push(encodeURIComponent(key) + "=" + encodeURIComponent(data[key]));
                            }
                        }
                        return queryString.join("&");
                    }
                }
            })
        }
    };

    common.factory("userAccount", ["APP_HOST","$resource", userAccount]);
})();