using System.Collections.Generic;
using System.Threading.Tasks;

using Furion.DataEncryption;

using Microsoft.AspNetCore.Mvc;

using QianShiChat.Server.Constants;
using QianShiChat.Server.Services.FileService;
using QianShiChat.Server.Services.User;

namespace QianShiChat.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        public UserController(IUserService userService,
            IFileService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromForm] LoginInput input)
        {
            var userInfo = await _userService.LoginAsync(input);

            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
            {
                { ClaimConstant.UserId, userInfo.Id },
                { ClaimConstant.UserName, userInfo.NickName }
            });

            return Ok(new
            {
                User = new
                {
                    userInfo.NickName,
                    Avatar = await _fileService.GetFileUrlAsync(userInfo.Avatar)
                },
                AccessToken = accessToken
            });
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisteredAsync([FromForm] RegisteredInput input)
        {
            var userInfo = await _userService.RegisteredAsync(input);

            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
            {
                { ClaimConstant.UserId, userInfo.Id },
                { ClaimConstant.UserName, userInfo.NickName }
            });

            return Ok(new
            {
                User = new
                {
                    userInfo.NickName,
                    Avatar = await _fileService.GetFileUrlAsync(userInfo.Avatar)
                },
                AccessToken = accessToken
            });
        }
    }
}
