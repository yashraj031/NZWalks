using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Model.DTO;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpGet]    
        public async Task<IActionResult> GetAllAsync()
        {
            //Fetch Data From Domain
            var walkDomain = await walkRepository.GetAllAsync();

            //convert Domain walks to DTO Walks
            var walkDTO = mapper.Map<List<Model.DTO.Walk>>(walkDomain);
            //Return response
            return Ok(walkDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkAsunc(Guid id)
        { 
            // Get walk Domain Object from Database
            var walkDomain = await walkRepository.GetAsync(id);

            // Convert Domain Object to DTO
            var walkDTO = mapper.Map<Model.DTO.Walk>(walkDomain);

            // Return Response
            return Ok(walkDTO);
        }

        [HttpPost]
        
        public async Task<IActionResult> AddWalkAsync([FromBody] Model.DTO.AddWalkRequest addWalkRequest)
        {
            // Request(DTO) to Domain Model
            var walkDomain = new Model.Domain.Walk
            {                
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyID = addWalkRequest.WalkDifficultyID
            };
            // Pass Details to Repository
            walkDomain = await walkRepository.AddAsync(walkDomain);

            // Convert Back to DTO
            var walkDTO = new Model.Domain.Walk()
            {
                Id = walkDomain.Id,
                Name = walkDomain.Name,
                Length = walkDomain.Length,
                RegionId = walkDomain.RegionId,
                WalkDifficultyID = walkDomain.WalkDifficultyID
            };
            return CreatedAtAction(nameof(GetAllAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // Get Region from Database
            var walkDomain = await walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<Model.DTO.Walk>(walkDomain);
            
            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdareWalkAsync([FromRoute] Guid id, [FromBody] Model.DTO.UpdateWalkRequest updateWalkRequest)
            {
                // Request(DTO) to Domain Model
                var walkDomain = new Model.Domain.Walk
                {
                    Name = updateWalkRequest.Name,
                    Length = updateWalkRequest.Length,
                    RegionId = updateWalkRequest.RegionId,
                    WalkDifficultyID = updateWalkRequest.WalkDifficultyID,
                };
                // Pass Details to Repository
                walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

                // If null then not found
                if (walkDomain == null)
                {
                    return NotFound();

                }

                // Convert Back to DTO
                var walkDTO = new Model.Domain.Walk
                {
                    Name = walkDomain.Name,
                    Length = walkDomain.Length,
                    RegionId = walkDomain.RegionId,
                    WalkDifficultyID = walkDomain.WalkDifficultyID,
                };
                return Ok(walkDTO);
            }
        }
}
