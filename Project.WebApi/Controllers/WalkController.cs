using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.WebApi.Models.Domain;
using Project.WebApi.Models.DTO;
using Project.WebApi.Repositories;

namespace Project.WebApi.Controllers
{
    //because we are using the API controller tag
    //when the execution come to this 
    //it uses this tag to check the model state is valid or invalid (FluentValidation)
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddAsync(AddWalkRequest request)
        {
            // Validate the incoming request
            if (!(await ValidateAddWalkAsync(request)))
            {
                return BadRequest(ModelState);
            }
            var walk = _mapper.Map<Walk>(request);
            walk = await _walkRepository.AddAsync(walk);
            var walkDto = _mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,
                                                            [FromBody] UpdateWalkRequest request)
        {
            //Validate the incoming request
            if (!(await ValidateUpdateWalkAsync(request)))
            {
                return BadRequest(ModelState);
            }

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

        #region private method to validate incoming request

        private async Task<bool> ValidateAddWalkAsync(AddWalkRequest addWalkRequest)
        {
            //if (addWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest),
            //        $"{nameof(addWalkRequest)} cannot be empty.");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Name),
            //        $"{nameof(addWalkRequest.Name)} is required.");
            //}

            //if (addWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Length),
            //        $"{nameof(addWalkRequest.Length)} should be greater than zero.");
            //}

            var region = await _regionRepository.GetAsync(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                    $"{nameof(addWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await _walkDifficultyRepository.GetAsync(addWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                       $"{nameof(addWalkRequest.WalkDifficultyId)} is invalid.");

            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync(UpdateWalkRequest updateWalkRequest)
        {
            //if (updateWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest),
            //        $"{nameof(updateWalkRequest)} cannot be empty.");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Name),
            //        $"{nameof(updateWalkRequest.Name)} is required.");
            //}

            //if (updateWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Length),
            //        $"{nameof(updateWalkRequest.Length)} should be greater than zero.");
            //}

            var region = await _regionRepository.GetAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId),
                    $"{nameof(updateWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await _walkDifficultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId),
                       $"{nameof(updateWalkRequest.WalkDifficultyId)} is invalid.");

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
