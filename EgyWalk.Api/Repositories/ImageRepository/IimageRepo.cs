using EgyWalk.Api.Models.Domain;

namespace EgyWalk.Api.Repositories.ImageRepository
{
    public interface IimageRepo
    {


        Task<Image> Uploud(Image image);
    }
}
