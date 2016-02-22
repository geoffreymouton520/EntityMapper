(function () {
    "use strict";
    var common = angular.module("common.services");

    var domainResource = function (appHost, $resource) {
        return $resource(appHost + "/api/domains/:id", {
            id: "@id"
        }, {
            update: {
                method: "PUT" // this method issues a PUT request
            }
        });
    };

    common.factory("domainResource", ["APP_HOST", "$resource", domainResource]);
})();