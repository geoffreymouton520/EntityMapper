(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");
    var loginCtrl = function (userAccount, currentUser,$state) {
        var vm = this;
        vm.isLoggedIn = false;
        vm.UserData = {
            email: "",
            password: ""
        };
        vm.login = function () {
            //vm.userData.confirmPassword = vm.userData.password;
            vm.userData.grant_type = "password";
            vm.userData.userName = vm.userData.email;
            vm.isLoggedIn = function() {
                return currentUser.getProfile().isLoggedIn;
            };

            userAccount.login.loginUser(vm.userData,
                function (data) {
                    vm.userData.confirmPassword = "";
                    vm.token = data.access_token;
                    currentUser.setProfile(vm.userData.userName, data.access_token);
                    toastr.success("Login Successfull");
                    $state.go("dashboard");
                },
                function (response) {
                    vm.password = "";
                    var message ="";
                    if (response.data) {
                        if (response.data.error_description) {
                            message = response.data.error_description + "\r\n";
                        }
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
                    }

                    toastr.error(message);
                });
        };
    };


    mapperClient.controller("loginCtrl", ["userAccount", "currentUser", "$state", loginCtrl]);
})();
