(function() {
    "use strict";
    var mapperClient = angular.module("mapperClient", ["common.services",
        "ui.router",
        "frapontillo.bootstrap-switch",
        "ngMessages",
        "hSweetAlert",
        "ngAnimate",
        "toastr",
        "trNgGrid"
    ]);

    mapperClient.constant("APP_HOST", "http://localhost:53652");
})();
