using AutoMapper;
using EgyWalk.Api.Dtos.WalkDtos;
using EgyWalk.Api.Models.Domain;
using EgyWalk.Api.Repositories.WalkRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EgyWalk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WalkController> _log;

        public WalkController(IWalkRepository walkRepository , IMapper mapper , ILogger<WalkController> Log)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
            _log = Log;
        }


        // get all walks 
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll([FromQuery]string? filterQury , string? sortBy, bool? isAscending, int pageNumber = 1, int pageSize = 1000)
        {
            try
            {
                _log.LogInformation("User use A get all method");
                var Walks = await _walkRepository.GetAllAsync(filterQury, sortBy, isAscending ?? true, pageNumber, pageSize);
                return Ok(_mapper.Map<List<ReadWalkDto>>(Walks));
            }
            catch (Exception ex )
            {

                _log.LogError(ex ,ex.Message);

                throw;
            }
           

            

         
        }
        // get all walks by id
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var walk = await _walkRepository.GetAsync(Id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ReadWalkDto>(walk));
        }


        // add new walk
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Post(AddWalkDto walkDto)
        {
            var walks = await _walkRepository.AddAsync(_mapper.Map<Walk>(walkDto));

            return Ok(_mapper.Map<ReadWalkDto>(walks));
        }

        // Delete a walk 
        [HttpDelete]
        [Route("Delete/{Id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var walk = await _walkRepository.DeleteAsync(Id);
            if (walk == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<ReadWalkDto>(walk));
        }

        // update a walk 
        [HttpPut]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid Id , AddWalkDto walkDto)
        {
            var walk = await _walkRepository.Update(Id, _mapper.Map<Walk>(walkDto));
            if (walk == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<AddWalkDto>(walk));
        }

    }
}
