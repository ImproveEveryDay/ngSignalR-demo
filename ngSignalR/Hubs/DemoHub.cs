using AngularSignal.Tickers;
using Microsoft.AspNet.SignalR;

namespace AngularSignal.Hubs
{
    public class DemoHub : Hub
    {
        private readonly DemoTicker demoTicker;

        public DemoHub() : this(DemoTicker.Instance){}

        public DemoHub(DemoTicker ticker)
        {
            demoTicker = ticker;
        }

        public void Update()
        {
            demoTicker.Update(null);
        }
    }
}