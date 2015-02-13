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
    public class PerformanceTicker
    {
        private readonly static Lazy<PerformanceTicker> instance = new Lazy<PerformanceTicker>(
            () => new PerformanceTicker(GlobalHost.ConnectionManager.GetHubContext<PerformanceHub>().Clients)
        );

        private readonly TimeSpan updateInterval = TimeSpan.FromMilliseconds(1000);
        private readonly Timer timer;
        private volatile bool updating;
        private readonly object updateStateLock = new object();
        private readonly PerformanceCounter cpuCounter;
        private readonly PerformanceCounter ramCounter;
        private PerformanceInfo performanceInfo;

        public static PerformanceTicker Instance
        {
            get { return instance.Value; }
        }

        public Timer Timer
        {
            get { return timer; }
        }

        private IHubConnectionContext<dynamic> Clients { get; set; }

        private PerformanceTicker(IHubConnectionContext<dynamic> clients)
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

                    performanceInfo = new PerformanceInfo
                    {
                        DateTime = DateTime.Now,
                        CpuUsage = GetCurrentCpuUsage(),
                        RamAvailable = GetAvailableRam()
                    };

                    BroadcastMessage(performanceInfo);
                    updating = false;
                }
            }
        }

        public PerformanceInfo GetPerformanceInfo()
        {
            return performanceInfo;
        }

        public void BroadcastMessage(PerformanceInfo demoPayload)
        {
            Clients.All.update(PerformanceInfoDto.Map(demoPayload));
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