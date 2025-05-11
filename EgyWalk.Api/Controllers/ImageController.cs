using EgyWalk.Api.Dtos.ImageDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgyWalk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        public async Task<IActionResult> Uploud(UploudImageDto ImageUploudedDto)
        {

            return Ok();
        }


        private void CheckValidation(UploudImageDto ImageUploudedDto)
        {

        }

    }
}
