(function () {
    "use strict";
    var common = angular.module("common.services");

    var entityMappingResource = function (appHost, $resource) {
        return $resource(appHost + "/api/entityMappings/:id", { id: "@id" }, {
            update: {
                method: "PUT" // this method issues a PUT request
            },
            automate: {
                method: "POST",
                url: appHost + "/rpc/automation/automate",
                isArray:true
            }
        });
    };

    common.factory("entityMappingResource", ["APP_HOST", "$resource", entityMappingResource]);
})();