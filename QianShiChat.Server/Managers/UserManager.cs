using Furion.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Http;

using QianShiChat.Server.Models.Entities;

namespace QianShiChat.Server.Managers
{
    public class UserManager : IUserManager, IScoped
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ChatDbContext _context;
        public UserManager(IHttpContextAccessor httpContextAccessor,
            ChatDbContext chatDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = chatDbContext;
        }

        public int UserId => int.Parse(
            _httpContextAccessor.HttpContext.User?.FindFirst(x => x.Type.Equals(Constants.ClaimConstant.UserId))?.Value
            ?? "0");

        private UserInfo _userInfo;
        public UserInfo UserInfo => _userInfo ??= _context.UserInfos.FirstOrDefault(x => x.Id.Equals(UserId));
    }
}
