using NZWalks.API.Models.Domain;

namespace NZWalks.API.IRepositories
{
    public interface IRegionRepository
    {
        /// <summary>
        /// 查询所有接口
        /// </summary>
        /// <returns></returns>
        Task<List<Region>> GetAllAsync();

        /// <summary>
        /// 根据ID查询接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id,Region region);
        Task<Region?> DeleteAsync(Guid id);

    }
}
