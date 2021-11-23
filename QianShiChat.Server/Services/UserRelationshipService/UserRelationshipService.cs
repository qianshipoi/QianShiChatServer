using Furion.DependencyInjection;

using Microsoft.EntityFrameworkCore;

using QianShiChat.Server.Managers;
using QianShiChat.Server.Models.Entities;

namespace QianShiChat.Server.Services.UserRelationshipService
{
    public class UserRelationshipService : IUserRelationshipService, ITransient
    {
        private readonly ILogger<UserRelationshipService> _logger;
        private readonly ChatDbContext _context;
        private readonly IUserManager _userManager;

        public UserRelationshipService(ILogger<UserRelationshipService> logger,
            ChatDbContext context,
            IUserManager userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// 根据用户id和关注类型获取列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private IQueryable<UserRelationship> GetByFocusIdAndType(int userId, sbyte type)
            => _context.UserRelationships.Where(x => x.UserId == userId && x.FocusType == type);

        /// <summary>
        /// 根据用户编号获取好友列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IQueryable<UserInfo> GetFriends(int userId)
            => from ur in GetByFocusIdAndType(userId, 1)
               join u in _context.UserInfos on ur.FocusId equals u.Id into uTemp
               from user in uTemp.DefaultIfEmpty()
               select user;

        /// <summary>
        /// 判断用户之间是否是好友
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="toUserId"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserIsFriends(int userId, int toUserId)
            => await GetByFocusIdAndType(userId, 1).AsNoTracking().AnyAsync(x => x.FocusId == toUserId);

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserInfo>> GetFriendsList(int userId)
            => await GetFriends(userId).AsNoTracking().ToListAsync();

        public async Task<UserRelationship> AddUserRelationship(int userId, int focusId, sbyte focusType)
        {
            var ur = new UserRelationship()
            {
                CreateTime = System.DateTime.Now,
                FocusId = focusId,
                FocusType = focusType,
                IsDelete = false,
                UserId = userId
            };

            await _context.UserRelationships.AddAsync(ur);
            await _context.SaveChangesAsync();
            return ur;
        }

    }
}
