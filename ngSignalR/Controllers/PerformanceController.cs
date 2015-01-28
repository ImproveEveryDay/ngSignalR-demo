using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AngularSignal.Models;
using AngularSignal.Models.Dto;

namespace AngularSignal.Controllers
{
    public class PerformanceController : ApiController
    {
        private readonly PerformanceRepository performanceRepository;

        public PerformanceController()
        {
            performanceRepository = new PerformanceRepository();
        }

        [HttpGet]
        public PerformanceCounterCategoryDto[] GetCategories()
        {
            var list = performanceRepository.Categories;
            var dtos = list.Select(PerformanceCounterCategoryDto.Map).ToArray();
            return dtos;
        }

        [HttpGet]
        public List<HardriveDto> GetDrivesInfo()
        {
            var list = performanceRepository.GetDrivesInfo();
            var dtos = list.Select(HardriveDto.Map).ToList();
            return dtos;
        }
    }
}
