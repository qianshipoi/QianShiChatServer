using System.ComponentModel.DataAnnotations;

namespace QianShiChat.Server.Services.User
{
    public class LoginInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required, MaxLength(32)]
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required, MaxLength(64)]
        public string Password { get; set; }
    }
}
