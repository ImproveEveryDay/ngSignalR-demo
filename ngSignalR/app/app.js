
var appRoot = angular.module('main', ['ngRoute', 'ngGrid', 'ngResource', 'ngSignalR', 'easypiechart']);

appRoot.config(['$routeProvider', 'signalrProvider', '$logProvider',
function ($routeProvider, signalrProvider, $logProvider) {

  $routeProvider
  .when('/', {
    templateUrl: '/home/main',
    controller: 'MainController'
  })
  .when('/chat', {
    templateUrl: '/home/chat',
    controller: 'ChatController'
  })
  .otherwise({ redirectTo: '/' });
  
  $logProvider.debugEnabled(false);
  signalrProvider.setTransports(['webSockets', 'serverSentEvents', 'foreverFrame', 'longPolling']);
 
}])
.controller('RootController', ['$scope', '$route', '$routeParams', '$location',
function ($scope, $route, $routeParams, $location) {
  $scope.$on('$routeChangeSuccess', function () {
    $scope.activeViewPath = $location.path();
  });
}]);
