using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EgyWalk.Api.Data
{
    public class EgyWalkAthuDbContext : IdentityDbContext
    {
        public EgyWalkAthuDbContext(DbContextOptions<EgyWalkAthuDbContext> option) : base(option)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e",
                    ConcurrencyStamp = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e",
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = "c309fa92-2123-47be-b397-a1c77adb502c",
                    ConcurrencyStamp = "c309fa92-2123-47be-b397-a1c77adb502c",
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
