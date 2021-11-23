using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QianShiChat.Server.Models.Entities
{
    public class Online
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public string ConnectionId { get; set; }
        public DateTime LoginTime { get; set; }
        public UserInfo UserInfo { get; set; }
    }

    public class OnlineEntityTypeConfiguration : IEntityTypeConfiguration<Online>
    {
        public void Configure(EntityTypeBuilder<Online> builder)
        {
            builder.ToTable("online");
            builder.Property(p => p.Id)
                .IsRequired()
                .HasComment("在线表主键")
                .ValueGeneratedNever();
            builder.Property(p => p.UserId)
                .IsRequired()
                .HasComment("用户编号");
            builder.HasOne(p => p.UserInfo)
                .WithMany(u => u.Onlines)
                .HasForeignKey(p => p.UserId);
        }
    }
}
