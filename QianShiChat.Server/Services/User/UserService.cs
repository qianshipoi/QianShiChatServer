using System.IO;
using System.Threading.Tasks;

using Furion.DataEncryption;
using Furion.DependencyInjection;
using Furion.DistributedIDGenerator;
using Furion.FriendlyException;

using Microsoft.EntityFrameworkCore;

using QianShiChat.Server.Models.Entities;
using QianShiChat.Server.Services.FileService;

namespace QianShiChat.Server.Services.User
{
    public class UserService : IUserService, ITransient
    {
        private readonly ChatDbContext _db;
        private readonly IFileService _fileService;
        public UserService(ChatDbContext context,
            IFileService fileService)
        {
            _db = context;
            _fileService = fileService;
        }
        
        public async Task<UserInfo> LoginAsync(LoginInput input)
        {
            var user = await _db.UserInfos.AsNoTracking().FirstOrDefaultAsync(x => x.UserName.Equals(input.LoginName));

            if (user == null) throw Oops.Oh(ErrorCodes.U0002);

            if (!MD5Encryption.Compare(input.Password, user.Password))
            {
                throw Oops.Oh(ErrorCodes.U0003);
            }

            return user;
        }

        public async Task<UserInfo> RegisteredAsync(RegisteredInput input)
        {
            var filePath = string.Empty;
            if (input.Avatar != null)
            {
                filePath = $"\\avatar\\{IDGen.NextID().ToString().Replace("-",string.Empty)}{Path.GetExtension(input.Avatar.FileName)}";

                // 保存文件
                if (!await _fileService.SaveFileAsync(filePath, input.Avatar))
                {
                    throw Oops.Oh(ErrorCodes.E0001, "头像保存异常");
                }
            }
            using var tran = _db.Database.BeginTransaction();
            UserInfo user = null;
            try
            {
                user = await _db.UserInfos.FromSqlInterpolated($"select * from userinfo where nickname = {input.UserName} for update").FirstOrDefaultAsync();
                if (user != null)
                {
                    throw Oops.Oh(ErrorCodes.U0001);
                }

                user = new UserInfo()
                {
                    NickName = input.UserName,
                    UserName = input.UserName,
                    Password = MD5Encryption.Encrypt(input.Password),
                    Avatar = filePath
                };

                await _db.UserInfos.AddAsync(user);

                await _db.SaveChangesAsync();

                await tran.CommitAsync();
            }
            catch
            {
                await tran.RollbackAsync();
                throw;
            }
            return user;
        }
    }
}
