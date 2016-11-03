(function() {

    var app = angular.module('EasyNetworking',['ui.router','ngResource','ngMessages','ngPassword','ngFileUpload','ui.bootstrap', 'angularjs-dropdown-multiselect','angularModalService']);

    app.config(function($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise(function($injector){
                    var $state = $injector.get('$state');
                     $state.go('mainPage');
                  });
                $stateProvider
                    .state('mainPage', {
                                url: '/mainPage',
                                data: {
                                     roles: []
                                },
                                views: {
                                  'content@': {
                                    templateUrl: '../partials/mainPage.html',
                                    controller: 'MainPageCtrl'
                                  }
                                },
                                authenticate: false
                          })
                    .state('login', {
                        url: '/login',
                        data: {
                            roles: []
                        },
                        views: {
                            'content@': {
                                templateUrl: '../partials/login.html',
                                controller: 'LoginController'
                            }
                        },
                        authenticate: false
                    });

}).run(['$rootScope', '$state', '$stateParams','principal',
                function($rootScope, $state, $stateParams,principal) {
                  $rootScope.$on('$stateChangeStart', function(event, toState, toStateParams) {

                      if(!principal.isAuthenticated() && toState.authenticate){
                          console.log("unauthenticated access detected");
                          $state.transitionTo("login");
                          event.preventDefault();
                      }
                  });

                   $rootScope.$on('$stateChangeSuccess', function (event, toState,toParams, fromState, fromParams) {
                              $rootScope.isSameState = angular.equals(fromState.name,
                                  toState.name);
                          });
                }
          ]);

    })();
