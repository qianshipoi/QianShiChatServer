namespace QianShiChat.Common
{
    public class UserInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public byte[] Avatar { get; set; }

        public UserInfo(string name, byte[] avatar)
        {
            Name = name;
            Avatar = avatar;
        }

    }
}