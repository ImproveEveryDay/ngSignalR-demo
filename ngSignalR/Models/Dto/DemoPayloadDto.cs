using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AngularSignal.Models.Dto
{
    public class DemoPayloadDto
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("ramAvailable")]
        public string RamAvailable { get; set; }

        [JsonProperty("cpuUsage")]
        public string CpuUsage { get; set; }

        public static DemoPayloadDto Map(PerformanceInfo entity)
        {
            var dto = new DemoPayloadDto
                {
                    Date = entity.DateTime.ToLongDateString(),
                    Time = entity.DateTime.ToLongTimeString(),
                    CpuUsage = entity.CpuUsage.ToString(),
                    RamAvailable = entity.RamAvailable.ToString()
                };
            return dto;
        }
    }
}