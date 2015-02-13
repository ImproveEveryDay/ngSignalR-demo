using Newtonsoft.Json;

namespace AngularSignal.Models.Dto
{
    public class PerformanceInfoDto
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("ramAvailable")]
        public string RamAvailable { get; set; }

        [JsonProperty("cpuUsage")]
        public string CpuUsage { get; set; }

        public static PerformanceInfoDto Map(PerformanceInfo entity)
        {
            var dto = new PerformanceInfoDto
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