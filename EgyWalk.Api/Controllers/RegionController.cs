using AutoMapper;
using EgyWalk.Api.CustomFilter;
using EgyWalk.Api.Dtos.RegionDtos;
using EgyWalk.Api.Dtos.WalkDtos;
using EgyWalk.Api.Models.Domain;
using EgyWalk.Api.Repositories.RegionRepositroy;
using EgyWalk.Api.Repositories.WalkRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgyWalk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepo _regionRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionController> _log;

        public RegionController(IRegionRepo regionRepo, IMapper mapper, ILogger<RegionController> Log)
        {
            _regionRepo = regionRepo;
            _mapper = mapper;
            _log = Log;
        }


        // get all Regions 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] string? filterQury, string? sortBy, bool? isAscending, int pageNumber = 1, int pageSize = 1000)
        {

            var Regions = await _regionRepo.GetAllAsync(filterQury, sortBy, isAscending ?? true, pageNumber, pageSize);

            return Ok(_mapper.Map<List<ReadRegionDto>>(Regions));

        }
        // get all Region by id
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var region = await _regionRepo.GetAsync(Id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ReadRegionDto>(region));
        }


        // add new Region
        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Post(AddRegionDto RegionDto)
        {
            var Region = await _regionRepo.AddAsync(_mapper.Map<Region>(RegionDto));

            return Ok(_mapper.Map<ReadRegionDto>(Region));
        }

        // Delete a Region 
        [HttpDelete]
        [Route("Delete/{Id:Guid}")]
        [Authorize(Roles = "Writer")]


        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var Region = await _regionRepo.DeleteAsync(Id);
            if (Region == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReadRegionDto>(Region));
        }

        // update a Region 
        [HttpPut]
        [Route("Update/{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid Id, AddRegionDto RegionDto)
        {
            var Region = await _regionRepo.Update(Id, _mapper.Map<Region>(RegionDto));
            if (Region == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReadRegionDto>(Region));
        }



    }
}
