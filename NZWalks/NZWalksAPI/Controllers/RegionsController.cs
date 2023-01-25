using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NZWalksAPI.Model.Domain;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionrepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionrepository, IMapper mapper)
        {
            this.regionrepository = regionrepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions() 
        {
            var regions =await regionrepository.GetAllAsync();

            // Return DTO Region
            //var regionsDTO = new List<Model.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Model.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Name,
            //        Area = region.Area,
            //        last = region.last,
            //        Long = region.Long,
            //        Population = region.Population
            //    };
            //    regionsDTO.Add(regionDTO);

            //});

            var regionsDTO = mapper.Map<List<Model.DTO.Region>>(regions);

            return Ok(regionsDTO);



        }
       
    }
}
