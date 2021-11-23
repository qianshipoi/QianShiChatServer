using System;
using System.Linq;
using System.Threading.Tasks;

using Furion.DependencyInjection;
using Furion.FriendlyException;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using QianShiChat.Server.Hubs;
using QianShiChat.Server.Managers;
using QianShiChat.Server.Models.Entities;
using Furion;
using Microsoft.Extensions.DependencyInjection;
using QianShiChat.Server.Models.ViewModels;

namespace QianShiChat.Server.Services.MessageService
{
    public class DirectMessageService : IMessageService, ITransient
    {
        private readonly ILogger<DirectMessageService> _logger;
        private readonly ChatDbContext _context;
        private readonly IUserManager _userManager;

        public DirectMessageService(ILogger<DirectMessageService> logger,
            ChatDbContext context,
            IUserManager userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task AddMessage(string content, ContentType type, int toUserId, bool isSend = false)
        {
            if (_userManager.UserId == 0) throw Oops.Oh(ErrorCodes.U0005);
            DirectMessage dm = new DirectMessage()
            {
                Content = content,
                ContentType = type,
                CreateTime = DateTime.Now,
                Receiver = toUserId,
                SenderId = _userManager.UserId
            };
            await _context.DirectMessages.AddAsync(dm);
            await _context.SaveChangesAsync();

            if (isSend)
            {
                _ = SendMessage(dm);
            }
        }

        public async Task ReadMessage(long id, bool isSend = false)
        {
            if (_userManager.UserId == 0) throw Oops.Oh(ErrorCodes.U0005);
            var message = await _context.DirectMessages.FindAsync(id);
            if (message == null) throw Oops.Oh(ErrorCodes.DM0001);
            if (message.Receiver != _userManager.UserId) throw Oops.Oh(ErrorCodes.U0005);

            message.Read = true;
            await _context.SaveChangesAsync();

            if (isSend)
            {
                _ = SendMessage(message);
            }
        }

        public async Task DelMessage(long id, bool isSend = false)
        {
            if (_userManager.UserId == 0) throw Oops.Oh(ErrorCodes.U0005);
            var message = await _context.DirectMessages.FindAsync(id);
            if (message == null) throw Oops.Oh(ErrorCodes.DM0001);
            if (message.SenderId != _userManager.UserId) throw Oops.Oh(ErrorCodes.U0005);
            message.IsDelete = true;
            await _context.SaveChangesAsync();

            if (isSend)
            {
                _ = SendMessage(message);
            }
        }

        private Task SendMessage(DirectMessage dm)
            => Task.Factory.StartNew(() =>
            {
                Scoped.Create(async (_, scope) =>
                {
                    var service = scope.ServiceProvider;
                    var context = service.GetRequiredService<ChatDbContext>();
                    var hubContext = service.GetRequiredService<IHubContext<ChatHub, IChatClient>>();

                    foreach (var connectionId in context.Onlines.AsNoTracking().Where(x => x.UserId == dm.Receiver).Select(x => x.ConnectionId).ToList())
                    {
                        await hubContext.Clients.User(connectionId).ReceiveMessage(dm.SenderId, new MessageViewModel
                        {
                            Content = dm.Content,
                            Id = dm.Id,
                            CreateTime = dm.CreateTime,
                            Read = dm.Read,
                            ToUserId = dm.Receiver,
                            UserId = dm.SenderId,
                            Type = dm.ContentType
                        });
                    }
                });
            });
    }
}
