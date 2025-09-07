using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositorys.IRepositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="addWalkRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDto>(walkDomainModel));


        }


        [HttpGet]

        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,pageNumber,pageSize);

            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walksDomainModel = await walkRepository.GetByIdAsync(id);

            if (walksDomainModel == null)
            {
                return NotFound();
            }


            return Ok(mapper.Map<WalkDto>(walksDomainModel));
        }

        //更新
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {

            var malkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            malkDomainModel = await walkRepository.UpdateAsync(id, malkDomainModel);
            if (malkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(malkDomainModel));


        }




        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var deleteWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deleteWalkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deleteWalkDomainModel));
            //return NoContent();
        }












    }
}
