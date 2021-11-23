using QianShiChat.Server.Models.Entities;

namespace QianShiChat.Server.Managers
{
    public interface IUserManager
    {
        public int UserId { get; }

        public UserInfo UserInfo { get; }
    }
}
