using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDifficultyDomain = await walkDifficultyRepository.GetAllAsync();
            var walkDifficultyDTO = mapper.Map<List<Model.DTO.WalkDifficulty>>(walkDifficultyDomain); 
            return Ok(walkDifficultyDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficulty == null) 
            {
                return NotFound();
            }

            var walkDifficultyDTO = mapper.Map<Model.DTO.WalkDifficulty>(walkDifficulty);
            return Ok(walkDifficultyDTO);
        
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] Model.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //Reques DTO to Domain Model
            var walkDifficultyDomain = new Model.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code,
            };
            // Pass Domain to repository

            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            // Conver Domain To DTO

            var walkDifficultyDTO = mapper.Map<Model.DTO.WalkDifficulty>(walkDifficultyDomain);
            return CreatedAtAction(nameof(GetWalkDifficultyById), new {id = walkDifficultyDTO.Id}, walkDifficultyDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdareWalkDifficultyAsync([FromRoute] Guid id, [FromBody] Model.DTO.UpdateWalkDifficultRequest updateWalkDifficultRequest)
        {
            // Request(DTO) to Domain Model
            var walkDifficultyDomain = new Model.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultRequest.Code,                
            };
            // Pass Details to Repository
            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);

            // If null then not found
            if (walkDifficultyDomain == null)
            {
                return NotFound();

            }

            // Convert Back to DTO
            var walkDifficultyDTO = new Model.Domain.WalkDifficulty
            {
                Code = walkDifficultyDomain.Code,
            };
            return Ok(walkDifficultyDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            // Get Region from Database
            var walkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<Model.DTO.Walk>(walkDifficultyDomain);

            return Ok(walkDTO);
        }
    }
}
