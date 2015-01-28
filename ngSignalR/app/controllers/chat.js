appRoot.controller('ChatController', ['$scope', 'signalr','$filter',
function ($scope, signalr, $filter) {

  var chatProxy;
  var connection;

  $scope.model = {
    messages: [],
    username: null
  };

  var init = function () {
    $scope.openConnection();
  };

  $scope.openConnection = function () {
    var hub = signalr.createHubConnection('chatHub');

    connection = hub.connection;
    chatProxy = hub.proxy;
    
    signalr.logging(true, connection);

    signalr.receiveProxy(chatProxy, 'addMessage', function (username, message) {
      $scope.$apply(function () {
        $scope.model.messages.unshift({
          username: username,
          date: $filter('date')(new Date, 'EEE d MMM HH:mm:ss'),
          content: message
        });
      });
    });
    
    signalr.startHubConnection(connection)
    .then(function () {
      $scope.model.connectionOpen = true;
    });
  };

  $scope.sendMessage = function () {
    signalr.sendProxy(chatProxy, 'SendMessage', {
      username: $scope.model.username,
      message: $scope.model.message
    });
    $scope.model.message = undefined;
  };

  $scope.$on('$destroy', function () {
    signalr.stopConnection(connection);
  });

  init();
}]);