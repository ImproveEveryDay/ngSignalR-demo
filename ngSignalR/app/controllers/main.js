appRoot.controller('MainController', ['$scope', 'signalr', '$interval','$log',
function ($scope, signalr, $interval, $log) {

  var demoProxy;
  var connection;

  $scope.model = {
    connectionOpen: true,
    loading: true,
    totalRam: 16042,
    messages:[]
  };

  var init = function () {
    $scope.openConnection();
  };

  $scope.percent = 65;
  $scope.options = {
    animate: {
      duration: 500,
      enabled: true
    },
    barColor: '#ff9d00',
    scaleColor: '#BBB',
    trackColor: '#EEE',
    lineWidth: 5,
    lineCap: 'square'
  };

  $scope.openConnection = function () {
    var hub = signalr.createHubConnection('performanceHub');

    connection = hub.connection;
    demoProxy = hub.proxy;
    
    signalr.logging(true, connection);

    signalr.receiveProxy(demoProxy, 'update', function (demoPayload) {
      $scope.model.date = demoPayload.date;
      $scope.model.time = demoPayload.time;
      $scope.model.ramAvailable = demoPayload.ramAvailable;
      $scope.model.cpuUsage = demoPayload.cpuUsage;
      $scope.model.usedRam = getUsedRam();
      $scope.model.usedRamPercent = getUsedRamPercent();
      $scope.model.loading = false;
    });

    signalr.received( function () {
      $log.debug('Data received');
    }, connection);

    signalr.starting(function () {
      $log.debug('Connection up');
    }, connection);
    
    signalr.startHubConnection(connection)
     .then(function () {
       $scope.model.connectionOpen = true;
     });

    $scope.buildCpuChart();
  };

  $scope.closeConnection = function () {
    signalr.stopConnection(connection)
    .then(function () {
      $interval.cancel($scope.chartInterval);
      $scope.model.connectionOpen = false;
    });
  };

  $scope.sendMessage = function () {
    signalr.sendProxy(demoProxy, 'SendMessage', $scope.model.message);
  };

  var getUsedRam = function () {
    return $scope.model.totalRam - $scope.model.ramAvailable;
  };

  var getUsedRamPercent = function () {
    return (($scope.model.totalRam - $scope.model.ramAvailable) / $scope.model.totalRam) * 100;
  };

  $scope.buildCpuChart = function () {
    var chart = {
      title: {
        text: ''
      },
      credits: false,
      chart: {
        backgroundColor: "",
        height: 350,
        type: 'spline',
        animation: Highcharts.svg, // don't animate in old IE
        marginRight: 10,
        events: {
          load: function () {

            // set up the updating of the chart each second
            var series = this.series[0];
            $scope.chartInterval = $interval(function () {
              var x = (new Date()).getTime(), // current time
                  y = parseInt($scope.model.cpuUsage);

              series.addPoint([x, y], true, true);
            }, 1000);
          }
        }
      },
      colors: ["#ff9d00"],
      xAxis: {
        type: 'datetime',
        tickPixelInterval: 150
      },
      yAxis: {
        title: {
          text: 'Percent %',
          style: {
            color: "#FFF"
          }
        },
        plotLines: [{
          value: 100,
          width: 1
        }],
        minRange: 100,
        min: 0,
        max: 100
      },
      tooltip: {
        formatter: function () {
          return '<b>' + this.series.name + '</b><br/>' +
              Highcharts.dateFormat('%H:%M:%S', this.x) + '<br/>' +
              this.y + "%";
        }
      },
      legend: {
        enabled: false
      },
      exporting: {
        enabled: false
      },
      plotOptions: {
        spline: {
          lineWidth: 3,
          marker: {
            enabled: false
          },
          animation: false
        }
      },
      series: [{
        name: 'CPU Usage',
        data: (function () {
          // generate an array of random data
          var data = [],
              time = (new Date()).getTime(),
              i;

          for (i = -20; i <= 0; i++) {
            data.push({
              x: time + i * 1000,
              y: 5
            });
          }
          return data;
        })()
      }]
    };
    $scope.model.cpuChart = chart;
  };
  
  $scope.$on('$destroy', function () {
    signalr.stopConnection(connection);
  });

  init();
}]);