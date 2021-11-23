using System;
using System.Linq;
using System.Threading.Tasks;

using Furion.DependencyInjection;
using Furion.FriendlyException;

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using QianShiChat.Server.Extensions;
using QianShiChat.Server.Hubs;
using QianShiChat.Server.Managers;
using QianShiChat.Server.Models.Entities;
using QianShiChat.Server.Models.ViewModels;

namespace QianShiChat.Server.Services.ApplyForService
{
    public class ApplyForService : IApplyForService, ITransient
    {
        private readonly ChatDbContext _context;
        private readonly ILogger<ChatDbContext> _logger;
        private readonly IUserManager _userManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public ApplyForService(ChatDbContext context,
            ILogger<ChatDbContext> logger,
            IUserManager userManager,
            IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        /// <summary>
        /// 添加申请记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task AddApplyFor(ApplyForInput input)
        {
            if (_userManager.UserId == 0) throw new UnauthorizedAccessException();
            var nowTime = DateTime.Now;
            var applyFor = new ApplyFor()
            {
                CreateTime = nowTime,
                LastUpdateTime = nowTime,
                Remark = input.Remark,
                Status = ApplyStatus.Applied,
                UserId = _userManager.UserId
            };

            await using var tran = await _context.Database.BeginTransactionAsync();
            try
            {
                if (input.GroupId == 0)
                {
                    if (!await _context.UserInfos.AnyAsync(x => x.Id == input.UserId))
                    {
                        throw Oops.Oh(ErrorCodes.U0004);
                    }

                    FormattableString sql =
                        $"select * from apply_for where UserId = {_userManager.UserId} and ToUserId = {input.UserId} and Type = 1 and `Status` = 1 for update";
                    // 用户申请
                    var oldApplyFor = await _context.ApplyFors.FromSqlInterpolated(sql).SingleOrDefaultAsync();
                    if (oldApplyFor != null)
                    {
                        throw Oops.Oh(ErrorCodes.A0001);
                    }

                    applyFor.ToUserId = input.UserId;
                    applyFor.Type = ApplyType.User;
                }
                else
                {
                    // 群组申请
                    var groupInfo = await _context.ChatGroups.Where(x => x.Id == input.GroupId).Select(x => new
                    {
                        x.UserId,
                        x.Id
                    }).SingleOrDefaultAsync();
                    if (groupInfo == null)
                    {
                        throw Oops.Oh(ErrorCodes.G0001);
                    }
                    FormattableString sql =
                        $"select * from apply_for where UserId = {_userManager.UserId} and Group = {input.GroupId} and Type = 1 and `Status` = 2 for update";
                    var oldApplyFor = await _context.ApplyFors.FromSqlInterpolated(sql).SingleOrDefaultAsync();
                    if (oldApplyFor != null)
                    {
                        throw Oops.Oh(ErrorCodes.A0001);
                    }

                    applyFor.GroupId = input.GroupId;
                    applyFor.ToUserId = groupInfo.UserId;
                    applyFor.Type = ApplyType.Group;
                }

                await _context.ApplyFors.AddAsync(applyFor);
                await _context.SaveChangesAsync();

                await tran.CommitAsync();
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// 处理申请记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task HandleApplyFor(int id, ApplyStatus status)
        {
            if (_userManager.UserId == 0) throw new UnauthorizedAccessException();
            var applyFor = await _context.ApplyFors.FindAsync(id);

            if (applyFor == null) throw Oops.Oh(ErrorCodes.A0002);

            if (applyFor.Status != ApplyStatus.Applied)
            {
                throw Oops.Oh(ErrorCodes.A0001);
            }
            await using var tran = await _context.Database.BeginTransactionAsync();
            try
            {
                switch (status)
                {
                    case ApplyStatus.Passed:
                        var nowTime = DateTime.Now;
                        switch (applyFor.Type)
                        {
                            case ApplyType.User:
                                {
                                    var ur = new UserRelationship()
                                    {
                                        CreateTime = nowTime,
                                        FocusId = applyFor.ToUserId,
                                        FocusType = 1,
                                        UserId = _userManager.UserId
                                    };

                                    var ur2 = new UserRelationship()
                                    {
                                        CreateTime = nowTime,
                                        FocusId = _userManager.UserId,
                                        FocusType = 1,
                                        UserId = applyFor.ToUserId
                                    };
                                    await _context.UserRelationships.AddRangeAsync(ur, ur2);
                                }
                                break;
                            case ApplyType.Group:
                                {
                                    await _context.UserRelationships.AddAsync(new UserRelationship()
                                    {
                                        CreateTime = nowTime,
                                        FocusId = applyFor.GroupId ?? 0L,
                                        FocusType = 2,
                                        UserId = _userManager.UserId
                                    });
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break; // 建立好友关系
                    case ApplyStatus.Rejected:
                    case ApplyStatus.Ignored:
                    case ApplyStatus.Applied:
                        break;
                }
                applyFor.Status = status;
                applyFor.LastUpdateTime = DateTime.Now;
                await _context.SaveChangesAsync();
                await tran.CommitAsync();
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
