using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QianShiChat.Server.Models.Entities
{
    /// <summary>
    /// 申请表
    /// </summary>
    public class ApplyFor
    {
        /// <summary>
        /// 申请表主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 申请人编号
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 申请类型 0 用户  1 群组
        /// </summary>
        public ApplyType Type { get; set; }
        /// <summary>
        /// 被申请人编号
        /// </summary>
        public int ToUserId { get; set; }
        /// <summary>
        /// 被申请群编号
        /// </summary>
        public long? GroupId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 申请状态
        /// </summary>
        public ApplyStatus Status { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }

    public enum ApplyStatus
    {
        /// <summary>
        /// 已申请
        /// </summary>
        Applied = 1,
        /// <summary>
        /// 已通过
        /// </summary>
        Passed = 2,
        /// <summary>
        /// 已驳回
        /// </summary>
        Rejected = 3,
        /// <summary>
        /// 已忽略
        /// </summary>
        Ignored = 4,
    }

    public enum ApplyType
    {
        /// <summary>
        /// 用户
        /// </summary>
        User = 1,
        /// <summary>
        /// 群组
        /// </summary>
        Group = 2
    }

    public class ApplyForEntityTypeConfiguration : IEntityTypeConfiguration<ApplyFor>
    {
        public void Configure(EntityTypeBuilder<ApplyFor> builder)
        {
            builder.ToTable("apply_for");
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.UserId);
            builder.HasIndex(p => p.ToUserId);
            builder.HasIndex(p => p.GroupId);
            builder.Property(p => p.Id).ValueGeneratedOnAdd().HasComment("申请表主键");
            builder.Property(p => p.UserId).IsRequired().HasComment("申请用户");
            builder.Property(p => p.ToUserId).HasComment("被申请用户");
            builder.Property(p => p.GroupId).HasComment("被申请群组");
            builder.Property(p => p.Type).IsRequired().HasComment("申请类型 1 用户 2 群组");
            builder.Property(p => p.CreateTime).IsRequired().HasComment("申请时间");
            builder.Property(p => p.Remark).HasMaxLength(512).HasComment("申请备注");
            builder.Property(p => p.Status).IsRequired().HasComment("申请状态 1 已申请 2 已通过 3 已驳回 4 已忽略 ");
            builder.Property(p => p.LastUpdateTime).HasComment("最后修改时间");
        }
    }
}
