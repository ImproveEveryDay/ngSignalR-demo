appRoot.service('performanceService', ['$http',
function ($http) {
  this.getCategories = function () {
    return $http.get('api/performance/getcategories').then(function (result) {
      return result.data;
    });
  };
  
  this.getDrives = function () {
    return $http.get('api/performance/getdrivesinfo').then(function (result) {
      return result.data;
    });
  };
}]);