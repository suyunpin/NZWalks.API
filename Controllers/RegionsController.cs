using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.IRepositories;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;


namespace NZWalks.API.Controllers
{
    /// <summary>
    /// https://地址：端口/api/控制器名字
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        //private readonly NZWalksDbContext dbContext = dbContext;

        //private readonly NZWalksDbContext dbContext;

        //public RegionsController(NZWalksDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, NZWalksDbContext dbContext, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }




        #region 查询所有
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

            //var regionsDto = new List<RegionDto>();
            ////注意var regionDomain in regionsDomain的第一个自定义参数
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl,
            //        Time = regionDomain.Time,

            //    });

            //}

            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regionsDto);

        }
        #endregion 



        #region 根据id查询
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET:https://localhost:端口/api/Regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //检查id是否存在
            //var regionDomain =await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //    Time = regionDomain.Time,
            //};

            return Ok(mapper.Map<RegionDto>(regionDomain));

        }

        #endregion


        #region 添加
        [HttpPost]
        [ValidateModel]
        
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {


            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

           
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            
            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //    Time = regionDomainModel.Time,
            //};


            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);



        }
        #endregion


        #region 更新数据 

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }


            //var regionDto = mapper.Map<RegionDto>(regionDomainModel);




            return Ok(mapper.Map<RegionDto>(regionDomainModel));


        }
        #endregion


        #region 删除

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //var regionDomainModel = await regionRepository.DeleteAsync(id);
            //if (regionDomainModel == null)
            //{
            //    return NotFound();
            //}


            //return Ok(mapper.Map<RegionDto>(regionDomainModel));

            //删除不返回数据 状态码204
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }


            return NoContent();

        }


        #endregion
    }
}
