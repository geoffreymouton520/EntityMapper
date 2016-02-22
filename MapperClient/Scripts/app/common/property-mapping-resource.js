(function () {
    "use strict";
    var common = angular.module("common.services");

    var propertyMappingResource = function (appHost, $resource) {
        return $resource(appHost + "/api/propertyMappings/:id", { id: "@id" }, {
            update: {
                method: "PUT" // this method issues a PUT request
            }
        });
    };

    common.factory("propertyMappingResource", ["APP_HOST", "$resource", propertyMappingResource]);
})();