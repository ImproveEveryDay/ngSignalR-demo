using System;
using System.Diagnostics;
using System.Threading;
using AngularSignal.Hubs;
using AngularSignal.Models;
using AngularSignal.Models.Dto;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AngularSignal.Tickers
{
    public class DemoTicker
    {
        private readonly static Lazy<DemoTicker> instance = new Lazy<DemoTicker>(
            () => new DemoTicker(GlobalHost.ConnectionManager.GetHubContext<DemoHub>().Clients)
        );

        private readonly TimeSpan updateInterval = TimeSpan.FromMilliseconds(1000);
        private readonly Timer timer;
        private volatile bool updating;
        private readonly object updateStateLock = new object();
        private readonly PerformanceCounter cpuCounter;
        private readonly PerformanceCounter ramCounter;

        public static DemoTicker Instance
        {
            get { return instance.Value; }
        }

        public Timer Timer
        {
            get { return timer; }
        }

        private IHubConnectionContext<dynamic> Clients { get; set; }

        private DemoTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            cpuCounter = new PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            timer = new Timer(Update, null, updateInterval, updateInterval);
        }

        public void Update(object state)
        {
            lock (updateStateLock)
            {
                if (!updating)
                {
                    updating = true;

                    var demoPayload = new DemoPayload
                    {
                        DateTime = DateTime.Now,
                        CpuUsage = GetCurrentCpuUsage(),
                        RamAvailable = GetAvailableRam()
                    };

                    BroadcastMessage(demoPayload);
                    updating = false;
                }
            }
        }

        public void BroadcastMessage(DemoPayload demoPayload)
        {
            Clients.All.update(DemoPayloadDto.Map(demoPayload));
        }

        private int GetCurrentCpuUsage()
        {
            return (int)cpuCounter.NextValue();
        }

        private int GetAvailableRam()
        {
            return (int)ramCounter.NextValue();
        }
    }
}