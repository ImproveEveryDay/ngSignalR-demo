using System;
using System.Diagnostics;

namespace AngularSignal.Models
{
    public class DemoPayload
    {
        public DateTime DateTime { get; set; }
        public int CpuUsage { get; set; }
        public int RamAvailable { get; set; }
    }
}