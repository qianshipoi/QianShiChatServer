using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QianShiChat.Server.Models.Entities
{
    /// <summary>
    /// 私信记录
    /// </summary>
    public class DirectMessage
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        public int SenderId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public ContentType ContentType { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public int Receiver { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Read { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDelete { get; set; }
    }

    public enum ContentType
    {
        Text = 1,
        Image = 2
    }

    public class DirectMessageEntityTypeConfiguration : IEntityTypeConfiguration<DirectMessage>
    {
        public void Configure(EntityTypeBuilder<DirectMessage> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.SenderId);
            builder.HasIndex(p => p.Receiver);
            builder.ToTable("direct_message");
            builder.Property(p => p.Id)
                .IsRequired()
                .HasComment("私信表主键")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd();
            builder.Property(p => p.SenderId)
                .IsRequired()
                .HasComment("发送者");
            builder.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(1024)
                .HasComment("内容");
            builder.Property(p => p.ContentType)
                .IsRequired()
                .HasComment("内容类型 1 文字  2 图片");
            builder.Property(p => p.Receiver)
                .IsRequired()
                .HasComment("接收者");
            builder.Property(p => p.Read)
                .IsRequired()
                .HasComment("是否已读");
            builder.Property(p => p.CreateTime)
                .IsRequired()
                .HasComment("发送时间");
            builder.Property(p => p.IsDelete)
                .IsRequired()
                .HasComment("软删除");
        }
    }

}
