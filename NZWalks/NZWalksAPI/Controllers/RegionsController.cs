using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NZWalksAPI.Model.Domain;
using NZWalksAPI.Model.DTO;
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
            var regions = await regionrepository.GetAllAsync();

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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]

        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var regions = await regionrepository.GetAsync(id);

            if (regions == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Model.DTO.Region>(regions);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Model.DTO.AddRegionRequest addRegionRequest)
        {
            // Request(DTO) to Domain Model
            var region = new Model.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                last = addRegionRequest.last,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population
            };
            // Pass Details to Repository
            region = await regionrepository.AddAsync(region);

            // Convert Back to DTO
            var regionDTO = new Model.Domain.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                last = region.last,
                Long = region.Long,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Get Region from Database
            var region = await regionrepository.DeleteAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = new Model.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                last = region.last,
                Long = region.Long,
                Population = region.Population
            };
            return Ok(regionDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdareRegionAsync([FromRoute] Guid id, [FromBody] Model.DTO.UpdateRegionRequest updateRegionRequest)
        {
            // Request(DTO) to Domain Model
            var region = new Model.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                last = updateRegionRequest.last,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };
            // Pass Details to Repository
            region = await regionrepository.UpdateAsync(id, region);

            // If null then not found
            if (region == null)
            {
                return NotFound();
            
            }

            // Convert Back to DTO
            var regionDTO = new Model.Domain.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                last = region.last,
                Long = region.Long,
                Population = region.Population
            };
            return Ok(regionDTO);
        }

    }
}
