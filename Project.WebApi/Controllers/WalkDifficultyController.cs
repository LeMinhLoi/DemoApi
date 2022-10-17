using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.WebApi.Models.Domain;
using Project.WebApi.Models.DTO;
using Project.WebApi.Repositories;

namespace Project.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]//say that route name api = name controller
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;
        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<WalkDifficultyDto>>(await _walkDifficultyRepository.GetAllAsync()));
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.GetAsync(id);
            if (walkDifficulty == null)
            {
                return NotFound();
            }
            var walkDifficultyDto = _mapper.Map<WalkDifficultyDto>(walkDifficulty);
            return Ok(walkDifficultyDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyRequest request)
        {
            var walkDifficulty = _mapper.Map<WalkDifficulty>(request);
            walkDifficulty = await _walkDifficultyRepository.UpdateAsync(id, walkDifficulty);
            if(walkDifficulty == null)
            {
                return NotFound();
            }
            var walkDifficultyDto = _mapper.Map<WalkDifficultyDto>(walkDifficulty);
            return Ok(walkDifficultyDto);
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var o = await _walkDifficultyRepository.DeleteAsync(id);
            return Ok(_mapper.Map<WalkDifficultyDto>(o));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddWalkDifficultyRequest request)
        {
            var walkDifficulty = _mapper.Map<WalkDifficulty>(request);
            walkDifficulty = await _walkDifficultyRepository.AddAsync(walkDifficulty);
            var walkDifficultyDto = _mapper.Map<WalkDifficultyDto>(walkDifficulty);
            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficultyDto.Id }, walkDifficultyDto);
        }
    }
}
