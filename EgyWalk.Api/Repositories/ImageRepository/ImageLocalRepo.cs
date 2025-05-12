using EgyWalk.Api.Data;
using EgyWalk.Api.Models.Domain;

namespace EgyWalk.Api.Repositories.ImageRepository
{
    public class ImageLocalRepo : IimageRepo
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EgyWalkDbContext _context;

        public ImageLocalRepo(IWebHostEnvironment webHostEnvironment , IHttpContextAccessor httpContextAccessor , EgyWalkDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<Image> Uploud(Image image)
        {
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath,"Images" ,$"{image.FileName}{image.FileExtension}" );

            // copy image in images folder
            using var stream = new FileStream(localPath, FileMode.Create);

            await image.File.CopyToAsync(stream);




            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();


            return image;
        }
    }
}
