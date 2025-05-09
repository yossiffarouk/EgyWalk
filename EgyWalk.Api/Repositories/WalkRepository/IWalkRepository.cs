using EgyWalk.Api.Models.Domain;

namespace EgyWalk.Api.Repositories.WalkRepository
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk?> GetAsync(Guid Id);
        Task<Walk?> AddAsync(Walk walk);
        Task<Walk?> Update(Guid Id , Walk walk);
        Task<Walk?> DeleteAsync(Guid Id );

    }
}
