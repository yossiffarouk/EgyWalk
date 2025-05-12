using EgyWalk.Api.Models.Domain;

namespace EgyWalk.Api.Repositories.RegionRepositroy
{
    public interface IRegionRepo
    {

        Task<IEnumerable<Region>> GetAllAsync(string? filterQury = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Region?> GetAsync(Guid Id);
        Task<Region?> AddAsync(Region region);
        Task<Region?> Update(Guid Id, Region region);
        Task<Region?> DeleteAsync(Guid Id);
    }
}
