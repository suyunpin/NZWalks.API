using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Difficulty> Difficultys { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>()
            {
                new()
                {
                    Id = Guid.Parse("d60ec7cf-c3a9-4cd2-9a1b-17c32e45fa86"),
                    Name = "简单"
                },
                new()
                {
                    Id = Guid.Parse("846fc720-1d0b-404a-a7a3-ff54b5f270ff"),
                    Name = "中等"
                },
                new()
                {
                    Id = Guid.Parse("51887f3e-f104-4c82-91d3-3f7815ae4e70"),
                    Name = "困难"
                },
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>
            {
                new()
                {
                    Id = Guid.Parse("a125c285-913f-4335-8511-a06b2f6cf804"),
                    Name = "柳州",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new()
                {
                    Id = Guid.Parse("680bf996-d739-4e6a-b77b-6be944a3b922"),
                    Name = "桂林",
                    Code = "WGN",
                    RegionImageUrl = null
                },
                new()
                {
                    Id = Guid.Parse("c5ca4056-4056-4919-80e0-7952a82f6371"),
                    Name = "北京",
                    Code = "NSN",
                    RegionImageUrl = null
                },
                new()
                {
                    Id = Guid.Parse("58523b76-08cb-4823-bd55-cb85fb35b450"),
                    Name = "河池",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}