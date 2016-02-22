(function () {
    "use strict";
    var common = angular.module("common.services");

    var propertyResource = function (appHost, $resource) {
        return $resource(appHost + "/api/properties/:id", { id: "@id" }, {
            update: {
                method: "PUT" // this method issues a PUT request
            }
        });
    };

    common.factory("propertyResource", ["APP_HOST", "$resource", propertyResource]);
})();