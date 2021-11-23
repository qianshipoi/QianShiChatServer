using System.Collections.Concurrent;

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using QianShiChat.Server.Managers;
using QianShiChat.Server.Models.Entities;
using QianShiChat.Server.Models.ViewModels;
using QianShiChat.Server.Services.FileService;

namespace QianShiChat.Server.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly ChatDbContext _context;
        private readonly IUserManager _userManager;
        private readonly IFileService _fileService;
        public ChatHub(ChatDbContext context,
            IUserManager userManager,
            IFileService fileService)
        {
            _context = context;
            _userManager = userManager;
            _fileService = fileService;
        }

        /// <summary>
        /// 在线人员
        /// </summary>
        private readonly ConcurrentDictionary<int, string> _onlienUsers = new ConcurrentDictionary<int, string>();

        public override Task OnConnectedAsync()
        {
            Console.WriteLine(_userManager.UserId);
            _onlienUsers.AddOrUpdate(_userManager.UserId, Context.ConnectionId, (_, _) => Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _onlienUsers.TryRemove(_userManager.UserId, out string _);
            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserInfo>> GetUsersAsync()
        {
            return await _context.UserInfos.Select(x => new UserInfo
            {
                Id = x.Id,
                NickName = x.NickName,
                Avatar = _fileService.GetFileUrl(x.Avatar)
            }).ToListAsync();
        }

        public async Task<string> CreateOrJoinPrivateChannel(int userId)
        {
            string groupName = $"{_userManager.UserId}_{userId}";
            if (_userManager.UserId > userId)
            {
                groupName = $"{userId}_{_userManager.UserId}";
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).ReceiveMessage(_userManager.UserId, new MessageViewModel
            {
                Content = $"用户[{_userManager.UserId}]加入该聊天",
                CreateTime = DateTime.Now,
                UserId = _userManager.UserId,
            });
            Console.WriteLine($"用户[{_userManager.UserId}]加入该聊天");
            return groupName;
        }

        public async Task QuitPrivateChannel(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).ReceiveMessage(_userManager.UserId, new MessageViewModel
            {
                Content = $"用户[{_userManager.UserId}]离开该聊天",
                CreateTime = DateTime.Now,
                UserId = _userManager.UserId,
            });
            Console.WriteLine($"用户[{_userManager.UserId}]离开该聊天");
        }

        public async Task SendMessageToPrivateChannel(string group, string message)
        {
            await Clients.OthersInGroup(group).ReceiveMessage(_userManager.UserId, new MessageViewModel
            {
                Content = message,
                CreateTime = DateTime.Now,
                UserId = _userManager.UserId,
            });
            Console.WriteLine($"用户[{_userManager.UserId}]发送[{message}]");
        }
    }
}
