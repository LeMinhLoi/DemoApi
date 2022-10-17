using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.WebApi.Models.Domain;
using Project.WebApi.Models.DTO;
using Project.WebApi.Repositories;
using System.Data;

namespace Project.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalkController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IWalkRepository _walkRepository;

        public WalkController(IMapper mapper, IWalkDifficultyRepository walkDifficultyRepository, IRegionRepository regionRepository, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkDifficultyRepository = walkDifficultyRepository;
            _regionRepository = regionRepository;
            _walkRepository = walkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var walks = await _walkRepository.GetAllAsync();
            var walksDto = _mapper.Map<IEnumerable<WalkDto>>(walks);
            return Ok(walksDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddWalkRequest request)
        {
            var walk = _mapper.Map<Walk>(request);
            walk = await _walkRepository.AddAsync(walk);
            var walkDto = _mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            // call Repository to delete walk
            var walkDomain = await _walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDTO = _mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,
                                                            [FromBody] UpdateWalkRequest request)
        {
            // Validate the incoming request
            //if (!(await ValidateUpdateWalkAsync(updateWalkRequest)))
            //{
            //    return BadRequest(ModelState);
            //}

            // Convert DTO to Domain object
            var walk = _mapper.Map<Walk>(request);

            // Pass details to Repository - Get Domain object in response (or null)
            walk = await _walkRepository.UpdateAsync(id, walk);

            // Handle Null (not found)
            if (walk == null)
            {
                return NotFound();
            }

            // Convert back Domain to DTO
            var walkDto = _mapper.Map<WalkDto>(walk);


            // Return Response
            return Ok(walkDto);
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            // Get Walk Domain object from database
            var walkDomin = await _walkRepository.GetAsync(id);

            // Convert Domain object to DTO
            var walkDTO = _mapper.Map<WalkDto>(walkDomin);

            // Return response
            return Ok(walkDTO);
        }
    }
}
