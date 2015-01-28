using System.Diagnostics;

namespace AngularSignal.Models.Dto
{
    public class PerformanceCounterCategoryDto
    {
        public string CategoryHelp { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public string MachineName { get; set; }
        public string[] InstanceNames { get; set; }

        public static PerformanceCounterCategoryDto Map(PerformanceCounterCategory entity)
        {
            var dto = new PerformanceCounterCategoryDto
                {
                    CategoryHelp = entity.CategoryHelp,
                    CategoryName = entity.CategoryName,
                    CategoryType = entity.CategoryType.ToString(),
                    MachineName = entity.MachineName,
                    InstanceNames = entity.GetInstanceNames()
                  
                };
            return dto;
        }

    }
}