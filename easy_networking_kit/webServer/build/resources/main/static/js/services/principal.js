(function () {

    var app = angular.module('EasyNetworking');

    app.service('principal', function ($q, $http, $timeout, $window) {

      return {

                getIdentity: function () {
                    var identity = $window.JSON.parse($window.sessionStorage.getItem('identity'));
                    return identity;
                },
                isInRole: function (role) {
                    if(this.getIdentity()!=null){
                          var roles = this.getIdentity().roles;

                          if (!this.isAuthenticated() || !roles) return false;

                          for (var i = 0; i < roles.length; i++) {
                              if (roles[i].authority.indexOf(role) > -1) {
                                  return true;
                              }
                          }

                          return false;
                    }
                    else{
                          return false;
                    }
                  },
                isAuthenticated: function () {
                    var identity = $window.JSON.parse($window.sessionStorage.getItem('identity'));
                    if (identity) {
                        return true;
                    } else {
                        return false;
                    }
                 },
                 getCurrentUser: function () {
                       return $window.JSON.parse($window.sessionStorage.getItem('identity'));
                   },

                 authenticate: function (identity, callBack, errorCallBack) {
                     $http({
                         method: 'POST',
                         url: '/api/login',
                         data: "username=" + identity.username + "&password=" + identity.password,
                         headers: {
                             "Content-Type": "application/x-www-form-urlencoded"
                         }
                     }).success(function (data, status, headers, config) {
                         $http({
                             method: 'GET',
                             url: '/api/user/current',
                         }).success(function (data, status, headers, config) {
                             $window.sessionStorage.setItem('identity', $window.JSON.stringify(data));
                             if (callBack != null) {
                                 callBack();
                             }
                         });
                     }).error(function (data, status, headers, config) {
                         errorCallBack(data);
                         $window.sessionStorage.removeItem('identity');
                     });

                 },
                 logout: function (callBack) {
                                    $http({
                                        method: 'GET',
                                        url: '/api/logout',
                                    }).success(function (data, status, headers, config) {
                                    //delete the user data
                                        $window.sessionStorage.removeItem('identity');
                                        callBack();
                                    }).error(function (data, status, headers, config) {
                                        console.log("Error at logout!");
                                    });
                                }
                 };
              }
          );
})();


