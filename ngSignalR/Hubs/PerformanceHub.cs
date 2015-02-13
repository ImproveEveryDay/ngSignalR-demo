using AngularSignal.Models;
using AngularSignal.Tickers;
using Microsoft.AspNet.SignalR;

namespace AngularSignal.Hubs
{
    public class PerformanceHub : Hub
    {
        private readonly PerformanceTicker demoTicker;

        public PerformanceHub() : this(PerformanceTicker.Instance){}

        public PerformanceHub(PerformanceTicker ticker)
        {
            demoTicker = ticker;
        }

        public PerformanceInfo Update()
        {
            return demoTicker.GetPerformanceInfo();
        }
    }
}