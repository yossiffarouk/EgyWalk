using EgyWalk.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EgyWalk.Api.Data
{
    public class EgyWalkDbContext :DbContext
    {
        public EgyWalkDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }
    }
}
