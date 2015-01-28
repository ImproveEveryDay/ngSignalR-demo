appRoot.controller('PerformanceController', ['$scope', 'categories', function ($scope, categories) {
  $scope.model = {};
  $scope.init = function () {
    $scope.model.categories = categories;
  };

  $scope.init();
}]);