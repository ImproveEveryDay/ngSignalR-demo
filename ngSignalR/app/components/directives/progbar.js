appRoot.directive('progressBar', ['$timeout', function ($timeout) {
  return function (scope, element, attrs) {

    scope.$watch(attrs.percentDone, function (value) {
      $timeout(function () {
        $(element).css({ width: value + "%" });
      });
    });

  };
}]);