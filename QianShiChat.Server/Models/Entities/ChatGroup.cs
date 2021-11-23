using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QianShiChat.Server.Models.Entities
{
    /// <summary>
    /// 群组
    /// </summary>
    public class ChatGroup
    {
        public long Id { get; set; }
        /// <summary>
        /// 创建人 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 群名称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 群头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDelete { get; set; }
    }

    public class ChatGroupEntityTypeConfiguration : IEntityTypeConfiguration<ChatGroup>
    {
        public void Configure(EntityTypeBuilder<ChatGroup> builder)
        {
            builder.ToTable("chat_group");
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.UserId);
            builder.Property(p => p.Id).ValueGeneratedOnAdd().HasComment("群组表主键");
            builder.Property(p => p.NickName).HasMaxLength(32).HasComment("群昵称");
            builder.Property(p => p.CreateTime).IsRequired().HasComment("创建时间");
            builder.Property(p => p.Avatar).HasMaxLength(1024).HasComment("群头像");
            builder.Property(p => p.IsDelete).IsRequired().HasDefaultValue(false).HasComment("软删除");
        }
    }
}
