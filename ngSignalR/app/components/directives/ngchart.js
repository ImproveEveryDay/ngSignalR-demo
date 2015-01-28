//@params
//  chartObj: the HighCharts Object -  See http://api.highcharts.com/highcharts
appRoot
.directive('ngChart', function () {
  return {
    restrict: 'A',
    scope: {
      chartObj: '='
    },
    link: function (iScope, iElement, iAttrs) {

      function Draw(chartObj) {
        $(iElement).highcharts(chartObj);
      }

      function Destroy() {
        $(iElement).highcharts().destroy();
      }

      iScope.$watch('chartObj', function (newValue, oldValue) {
        if (newValue !== oldValue) {
          Destroy();
          Draw(newValue);
        }
      });

      Draw(iScope.chartObj);
    }
  };
});