(function () {
    "use strict";
    var common = angular.module("common.services");

    var entityResource = function (appHost, $resource) {
        return $resource(appHost + "/api/entities/:id", {
            id: "@id"
            
        }, {
            update: {
                method: "PUT" // this method issues a PUT request
            },
            getBySystem: {
                method: "GET",
                isArray: true,
                url: appHost + "/rpc/entities/getBySystem?systemId=:systemId"
            }
        });
    };

    common.factory("entityResource", ["APP_HOST", "$resource", entityResource]);
})();