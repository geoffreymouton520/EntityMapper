(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var registerCtrl = function ($state, userAccount) {
        var vm = this;
        vm.isLoggedIn = false;
        vm.userData = {
            userName:"",
            email: "",
            password: "",
            confirmPassword:""
        };
        vm.register = function() {
            //vm.userData.confirmPassword = vm.userData.password;
            userAccount.registration.registerUser(vm.userData, function(data) {
                vm.userData.confirmPassword = "";
                toastr.success("Registered Successfull");
                vm.login();
            }, function (response) {
                var message = response.statusText + "\r\n";
                if (response.data.modelState) {
                    for (var key in response.data.modelState) {
                        if (response.data.modelState.hasOwnProperty(key)) {
                            message += response.data.modelState[key];
                        }
                    }
                }

                if (response.data.exceptionMessage) {
                    message += response.data.exceptionMessage;
                }

                toastr.error(message);
            });
        };
        vm.login = function() {
            vm.userData.grant_type = "password";
            vm.userData.userName = vm.userData.email;

            userAccount.login.loginUser(vm.userData,
                function (data) {
                    vm.userData.confirmPassword = "";
                    vm.isLoggedIn = true;
                    vm.token = data.access_token;
                    toastr.success("Login Successfull");
                    vm.login();
                },
                function (response) {
                    vm.password = "";
                    vm.isLoggedIn = false;
                    var message = response.statusText + "\r\n";
                    if (response.data.modelState) {
                        for (var key in response.data.modelState) {
                            if (response.data.modelState.hasOwnProperty(key)) {
                                message += response.data.modelState[key];
                            }
                        }
                    }

                    if (response.data.exceptionMessage) {
                        message += response.data.exceptionMessage;
                    }

                    toastr.error(message);
                });
        };
    };


    mapperClient.controller("registerCtrl", ["$state","userAccount", registerCtrl]);
})();
