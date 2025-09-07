using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositorys.IRepositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenPepository tokenPepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenPepository tokenPepository)
        {
            this.userManager = userManager;
            this.tokenPepository = tokenPepository;
        }


        /// <summary>
        /// 注册管理员
        /// </summary>
        /// <param name="registerRequestDto"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("Reginster")]
        public async Task<IActionResult> Reginster([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("注册成功！！");
                    }
                }

            }

            return BadRequest(new
            {
                Message = "注册失败",
                Errors = identityResult.Errors.Select(e => new { e.Code, e.Description })
            });
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = tokenPepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }

                }
            }

            return BadRequest("Username or password incorrect");
        }


    }
}
