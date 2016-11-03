(function() {
    var app = angular.module('EasyNetworking');

    var LoginCtrl = function($scope, $rootScope, $state, principal) {

        // /////////////////////////////////////////
        // Get user info
        // ///////////////////////////////////////////

        $scope.isAuthenticated = function() {
            return principal.isAuthenticated();
        };

        $scope.isAdmin = function() {
            return principal.isInRole("admin");
        };

        // /////////////////////////////////////////
        // Login
        // ///////////////////////////////////////////

        $scope.loginCallback = function() {
            $rootScope.currentUser = principal.getCurrentUser();
            $state.go("mainPage");
        };

        $scope.login = function() {
            principal.authenticate($scope.identity, $scope.loginCallback, function(error) {
                $scope.loginError = true;
            });
        };

        // /////////////////////////////////////////
        // Logout
        // ///////////////////////////////////////////
        $scope.logoutCallback = function() {
            $rootScope.currentUser = null;
            $state.go("mainPage");
        };

        $scope.logout = function() {
            principal.logout($scope.logoutCallback);
        };
    };

    app.controller('LoginCtrl', ['$scope', '$rootScope', '$state', 'principal', LoginCtrl]);
})();