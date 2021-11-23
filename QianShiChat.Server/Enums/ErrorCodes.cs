using Furion.FriendlyException;

namespace QianShiChat.Server
{
    [ErrorCodeType]
    public enum ErrorCodes
    {
        /// <summary>
        /// 用户名已存在
        /// </summary>
        [ErrorCodeItemMetadata("用户名已存在")]
        U0001,
        /// <summary>
        /// 用户名不存在
        /// </summary>
        [ErrorCodeItemMetadata("用户名不存在")]
        U0002,
        /// <summary>
        /// 密码不正确
        /// </summary>
        [ErrorCodeItemMetadata("密码不正确")]
        U0003,
        /// <summary>
        /// 用户不存在
        /// </summary>
        [ErrorCodeItemMetadata("用户不存在")]
        U0004,
        /// <summary>
        /// 无权操作
        /// </summary>
        [ErrorCodeItemMetadata("无权操作")]
        U0005,
        /// <summary>
        /// 群组不存在
        /// </summary>
        [ErrorCodeItemMetadata("群组不存在")]
        G0001,

        /// <summary>
        /// 申请已存在
        /// </summary>
        [ErrorCodeItemMetadata("申请已存在")]
        A0001,
        /// <summary>
        /// 该记录不存在
        /// </summary>
        [ErrorCodeItemMetadata("该记录不存在")]
        A0002,
        /// <summary>
        /// 该记录已被处理
        /// </summary>
        [ErrorCodeItemMetadata("该记录已被处理")]
        A0003,
        /// <summary>
        /// 消息不存在
        /// </summary>
        [ErrorCodeItemMetadata("消息不存在")]
        DM0001,

        [ErrorCodeItemMetadata("程序异常：{0}")]
        E0001,

    }
}
