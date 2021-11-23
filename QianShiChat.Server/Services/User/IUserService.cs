using System.Threading.Tasks;

using QianShiChat.Server.Models.Entities;

namespace QianShiChat.Server.Services.User
{
    public interface IUserService
    {
        Task<UserInfo> RegisteredAsync(RegisteredInput input);

        Task<UserInfo> LoginAsync(LoginInput input);
    }
}
