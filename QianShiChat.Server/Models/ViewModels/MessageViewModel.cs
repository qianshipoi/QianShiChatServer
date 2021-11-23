using System;

using QianShiChat.Server.Models.Entities;

namespace QianShiChat.Server.Models.ViewModels
{
    public class MessageViewModel
    {
        /// <summary>
        /// 消息编号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 接受人
        /// </summary>
        public int ToUserId { get; set; }
        /// <summary>
        /// 消息发送时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public ContentType Type { get; set; }
        /// <summary>
        /// 已读
        /// </summary>
        public bool Read { get; set; }
    }
}
