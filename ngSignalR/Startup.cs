using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AngularSignal.Startup))]
namespace AngularSignal
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            var connectionConfig = new HubConfiguration();
            connectionConfig.EnableDetailedErrors = true;
            app.MapSignalR("/signalr", connectionConfig);
        }
    }
}

