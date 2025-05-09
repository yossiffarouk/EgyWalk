using EgyWalk.Api.Models.Domain;

namespace EgyWalk.Api.Repositories.WalkRepository
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync(string? filterQury = null ,  string ?sortBy = null , bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetAsync(Guid Id);
        Task<Walk?> AddAsync(Walk walk);
        Task<Walk?> Update(Guid Id , Walk walk);
        Task<Walk?> DeleteAsync(Guid Id );

    }
}
