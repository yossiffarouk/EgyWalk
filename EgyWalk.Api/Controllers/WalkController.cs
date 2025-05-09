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

        public WalkController(IWalkRepository walkRepository)
        {
            _walkRepository = walkRepository;
        }


        // get all walks 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          return  Ok( await _walkRepository.GetAllAsync());
        }
        // get all walks by id
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromBody]Guid Id)
        {
            return Ok(await _walkRepository.GetAsync(Id));
        }


        // add new walk
        [HttpPost]
        public async Task<IActionResult> Post(Walk walk)
        {

            return Ok(await _walkRepository.AddAsync(walk));
        }

        // Delete a walk 
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid Id)
        {

            return Ok(await _walkRepository.DeleteAsync(Id));
        }

        // update a walk 
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Guid Id , Walk walk)
        {

            return Ok(await _walkRepository.Update(Id, walk));
        }

    }
}
