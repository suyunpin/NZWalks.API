using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.IRepositories;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public RegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion =await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion==null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>
        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion= await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            };

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            existingRegion.Time = region.Time;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
