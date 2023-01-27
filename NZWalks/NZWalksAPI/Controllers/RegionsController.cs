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
            //validate the Request
            if (!ValidateAddRegionAsync(addRegionRequest)) ;
            {
                return BadRequest(ModelState);
            }


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
            // Validate incoming requets

            if (!ValidateUpdareRegionAsync(updateRegionRequest))
            {
                return BadRequest(ModelState);
            }
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

        #region Private Methode
        private bool ValidateAddRegionAsync(Model.DTO.AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), $"Add Region Data is required.");
                return false;
            }
            if (string.IsNullOrEmpty(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code), $"{nameof(addRegionRequest.Code)} cannot be null or empty od white space.");
            }
            if (string.IsNullOrEmpty(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name), $"{nameof(addRegionRequest.Name)} cannot be null or empty od white space.");
            }
            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), $"{nameof(addRegionRequest.Area)} cannot be less than or equal to zero.");
            }
            if (addRegionRequest.last <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.last), $"{nameof(addRegionRequest.last)} cannot be less than or equal to zero.");
            }
            if (addRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Long), $"{nameof(addRegionRequest.Long)} cannot be less than or equal to zero.");
            }
            if (addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population), $"{nameof(addRegionRequest.Population)} cannot be less than zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        private bool ValidateUpdareRegionAsync(Model.DTO.UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest), $"Add Region Data is required.");
                return false;
            }
            if (string.IsNullOrEmpty(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code), $"{nameof(updateRegionRequest.Code)} cannot be null or empty od white space.");
            }
            if (string.IsNullOrEmpty(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name), $"{nameof(updateRegionRequest.Name)} cannot be null or empty od white space.");
            }
            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area), $"{nameof(updateRegionRequest.Area)} cannot be less than or equal to zero.");
            }
            if (updateRegionRequest.last <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.last), $"{nameof(updateRegionRequest.last)} cannot be less than or equal to zero.");
            }
            if (updateRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Long), $"{nameof(updateRegionRequest.Long)} cannot be less than or equal to zero.");
            }
            if (updateRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population), $"{nameof(updateRegionRequest.Population)} cannot be less than zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        #endregion

    }
}