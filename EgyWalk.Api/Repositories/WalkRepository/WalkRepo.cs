using EgyWalk.Api.Data;
using EgyWalk.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EgyWalk.Api.Repositories.WalkRepository
{
    public class WalkRepo : IWalkRepository
    {
        private readonly EgyWalkDbContext _db;

        public WalkRepo(EgyWalkDbContext db)
        {
            _db = db;
        }

        public async Task<Walk?> AddAsync(Walk walk)
        {

            await _db.Walks.AddAsync(walk);
            await _db.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid Id)
        {
            var WalkToDelete = await _db.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (WalkToDelete == null)
            {
                return null;
            }
            _db.Walks.Remove(WalkToDelete);
            await _db.SaveChangesAsync();
            return WalkToDelete;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await _db.Walks.ToListAsync();
        }

        public async Task<Walk?> GetAsync(Guid Id)
        {
           
            var WalkFromDb = await _db.Walks.FirstOrDefaultAsync(x => x.Id == Id);

            if (WalkFromDb == null)
            {
                return null;
            }


            return WalkFromDb;
        }

        public async Task<Walk?> Update(Guid Id, Walk walk)
        {
            var WalkFromDb = _db.Walks.FirstOrDefault(x => x.Id == Id);
            if (WalkFromDb == null)
                return null;

            WalkFromDb.Name = walk.Name;
            WalkFromDb.Description = walk.Description;
            WalkFromDb.LengthInKm = walk.LengthInKm;
            WalkFromDb.WalkImageUrl = walk.WalkImageUrl;
            WalkFromDb.DifficultyId = walk.DifficultyId;
            WalkFromDb.RegionId = walk.RegionId;


            await _db.SaveChangesAsync();

            return WalkFromDb;



        }
    }
}
