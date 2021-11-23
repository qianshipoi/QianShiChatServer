using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QianShiChat.Server.Models.Entities
{
    /// <summary>
    /// 用户关系表
    /// </summary>
    public class UserRelationship
    {
        public long Id { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 关注的用户或群组
        /// </summary>
        public long FocusId { get; set; }
        /// <summary>
        /// 关注类型 1 用户 2 群组
        /// </summary>
        public sbyte FocusType { get; set; }
        /// <summary>
        /// 关注时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDelete { get; set; }
    }

    public class UserRelationshipEntityTypeConfiguration : IEntityTypeConfiguration<UserRelationship>
    {
        public void Configure(EntityTypeBuilder<UserRelationship> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.UserId);
            builder.ToTable("user_relationship");
            builder.Property(p => p.Id).HasComment("用户关系表主键");
            builder.Property(p => p.UserId).IsRequired().HasComment("用户");
            builder.Property(p => p.FocusId).IsRequired().HasComment("关注的编号");
            builder.Property(p => p.FocusType).IsRequired().HasComment("关注类型 1 用户 2 群组");
            builder.Property(p => p.CreateTime).IsRequired().HasComment("关注时间");
            builder.Property(p => p.IsDelete).IsRequired().HasDefaultValue(false).HasComment("软删除");
        }
    }
}
