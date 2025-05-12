using EgyWalk.Api.Data;
using EgyWalk.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EgyWalk.Api.Repositories.RegionRepositroy
{
    public class RegionRepo : IRegionRepo
    {

        private readonly EgyWalkDbContext _db;

        public RegionRepo(EgyWalkDbContext db)
        {
            _db = db;
        }

        public async Task<Region?> AddAsync(Region region)
        {

            await _db.Regions.AddAsync(region);
            await _db.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteAsync(Guid Id)
        {
            var RegionToDelete = await _db.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (RegionToDelete == null)
            {
                return null;
            }
            _db.Regions.Remove(RegionToDelete);
            await _db.SaveChangesAsync();
            return RegionToDelete;
        }

        public async Task<IEnumerable<Region>> GetAllAsync(string? filterQury, string? sortBy, bool isAscending, int pageNumber = 1, int pageSize = 1000)
        {
            var regions = _db.Regions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterQury))
            {
                regions = regions.Where(a => a.Name.Contains(filterQury));
                pageNumber = 1;
            }
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regions = isAscending ? regions.OrderBy(a => a.Name) : regions.OrderByDescending(a => a.Name);
                }
                else if (sortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    regions = isAscending ? regions.OrderBy(a => a.Code) : regions.OrderByDescending(a => a.Code);
                }

            }

            var skipedRows = (pageNumber - 1) * pageSize;

            return await regions.Skip(skipedRows).Take(pageSize).ToListAsync();
        }

        public async Task<Region?> GetAsync(Guid Id)
        {

            var RegionFromDb = await _db.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (RegionFromDb == null)
            {
                return null;
            }


            return RegionFromDb;
        }

        public async Task<Region?> Update(Guid Id, Region region)
        {
            var RegionFromDb = _db.Regions.FirstOrDefault(x => x.Id == Id);
            if (RegionFromDb == null)
                return null;

            RegionFromDb.Name = region.Name;
            RegionFromDb.Code = region.Code;
            RegionFromDb.RegionImageUrl = region.RegionImageUrl;
      


            await _db.SaveChangesAsync();

            return RegionFromDb;



        }
    }
}
