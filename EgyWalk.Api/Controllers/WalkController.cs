using AutoMapper;
using EgyWalk.Api.Dtos.WalkDtos;
using EgyWalk.Api.Models.Domain;
using EgyWalk.Api.Repositories.WalkRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgyWalk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalkController(IWalkRepository walkRepository , IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }


        // get all walks 
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string? filterQury , string? sortBy, bool? isAscending)
        {
            var Walks = await _walkRepository.GetAllAsync(filterQury , sortBy , isAscending?? true);

            

          return  Ok(_mapper.Map<List<ReadWalkDto>>(Walks));
        }
        // get all walks by id
        [HttpGet("{Id}")]
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
        public async Task<IActionResult> Post(AddWalkDto walkDto)
        {
            var walks = await _walkRepository.AddAsync(_mapper.Map<Walk>(walkDto));

            return Ok(_mapper.Map<ReadWalkDto>(walks));
        }

        // Delete a walk 
        [HttpDelete]
        [Route("{Id:Guid}")]
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
