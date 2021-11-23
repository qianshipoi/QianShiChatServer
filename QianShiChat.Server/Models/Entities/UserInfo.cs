using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QianShiChat.Server.Models.Entities
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class UserInfo
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }

        public IEnumerable<Online> Onlines { get; set; }
    }

    public class UserInfoEntityTypeConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.ToTable("userinfo");
            builder.Property(p => p.Id)
                .IsRequired()
                .HasComment("用户表主键")
                .ValueGeneratedNever();
            builder.Property(p => p.NickName)
                .IsRequired()
                .HasMaxLength(32)
                .HasComment("用户昵称");
            builder.Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(64)
                .HasComment("登录密码");
            builder.Property(p => p.Avatar)
                .HasMaxLength(512)
                .HasComment("用户头像");
            builder.Property(p => p.UserName)
                .HasMaxLength(64)
                .IsRequired()
                .HasComment("用户登录名");
        }
    }
}
