(function () {
    "use strict";
    var common = angular.module("common.services");

    var systemResource = function (appHost, $resource) {
        return $resource(appHost + "/api/systems/:id", {
            id: "@id"
        }, {
            update: {
                method: "PUT" // this method issues a PUT request
            },
            getByDomain: {
                method: "GET",
                isArray: true,
                url: appHost + "/rpc/systems/getByDomain?domainId=:domainId"
            }
        });
    };

    //var systemRpc = function (appHost, $resource) {
    //    return $resource(appHost + "/rpc/systems/:id", {
    //        domainId: "@domainId"
    //    }, {
    //        query: {
    //            method: "GET",
    //            url: appHost + "/rpc/systems/getByDomain",
    //            isArray:true
    //        }
    //    });
    //};

    common.factory("systemResource", ["APP_HOST", "$resource", systemResource]);
    //common.factory("systemRpc", ["APP_HOST", "$resource", systemRpc]);
})();