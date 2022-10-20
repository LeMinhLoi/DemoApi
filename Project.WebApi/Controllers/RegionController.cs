using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.WebApi.Models.Domain;
using Project.WebApi.Models.DTO;
using Project.WebApi.Repositories;

namespace Project.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]//say that route name api = name controller
    [Authorize]
    public class RegionController : Controller
    {
        private readonly IRegionRepository _regionRepository;   
        private readonly IMapper _mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllAsync();
            var regiosDto = _mapper.Map<IEnumerable<RegionDto>>(regions);
            return Ok(regiosDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await _regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest request)
        {
            var region = _mapper.Map<Region>(request);
            region = await _regionRepository.AddAsync(region);
            var regionDto = _mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDto.Id }, regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Get region from database
            var region = await _regionRepository.DeleteAsync(id);

            // If null NotFound
            if (region == null)
            {
                return NotFound();
            }
            //Convert response back to DTO
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest request)
        {
            var region = _mapper.Map<Region>(request);
            region = await _regionRepository.UpdateAsync(id, region);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }
    }
}