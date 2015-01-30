using System;

namespace AngularSignal.Models
{
    public class PerformanceInfo
    {
        public DateTime DateTime { get; set; }
        public int CpuUsage { get; set; }
        public int RamAvailable { get; set; }
    }
}