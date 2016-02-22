(function () {
    "use strict";
    var common = angular.module("common.services");

    var mappingOriginResource = function (appHost, $resource) {
        return $resource(appHost + "/api/mappingOrigins/:id", { id: "@id" }, {
            update: {
                method: "PUT" // this method issues a PUT request
            }
        });
    };

    common.factory("mappingOriginResource", ["APP_HOST", "$resource", mappingOriginResource]);
})();