using Azure.Core;
using EgyWalk.Api.Dtos.ImageDtos;
using EgyWalk.Api.Models.Domain;
using EgyWalk.Api.Repositories.ImageRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgyWalk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IimageRepo _imageRepo;

        public ImageController(IimageRepo ImageRepo)
        {
            _imageRepo = ImageRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Uploud([FromForm]UploudImageDto ImageUploudedDto)
        {
            CheckValidation(ImageUploudedDto);

            if (ModelState.IsValid)
            {
                var image = new Image
                {
                    File = ImageUploudedDto.File,
                    FileExtension = Path.GetExtension(ImageUploudedDto.File.FileName),
                    FileSizeInBytes = ImageUploudedDto.File.Length,
                    FileName = ImageUploudedDto.FileName,
                    FileDescription = ImageUploudedDto.FileDescription,
                };


                await _imageRepo.Uploud(image);

                return Ok("Your image uplouded !");

            }
         return BadRequest(ModelState);
        }


        private void CheckValidation(UploudImageDto ImageUploudedDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(ImageUploudedDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (ImageUploudedDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }

    }
}
