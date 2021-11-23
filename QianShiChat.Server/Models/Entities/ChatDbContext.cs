using Microsoft.EntityFrameworkCore;

namespace QianShiChat.Server.Models.Entities
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }


        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<Online> Onlines {  get; set; }

        public DbSet<DirectMessage> DirectMessages { get; set; }

        public DbSet<UserRelationship> UserRelationships { get; set; }

        public DbSet<ChatGroup> ChatGroups { get; set; }

        public DbSet<ApplyFor> ApplyFors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserInfoEntityTypeConfiguration().Configure(modelBuilder.Entity<UserInfo>());
            new OnlineEntityTypeConfiguration().Configure(modelBuilder.Entity<Online>());
            new DirectMessageEntityTypeConfiguration().Configure(modelBuilder.Entity<DirectMessage>());
            new UserRelationshipEntityTypeConfiguration().Configure(modelBuilder.Entity<UserRelationship>());
            new ChatGroupEntityTypeConfiguration().Configure(modelBuilder.Entity<ChatGroup>());
            new ApplyForEntityTypeConfiguration().Configure(modelBuilder.Entity<ApplyFor>());

            base.OnModelCreating(modelBuilder);
        }

    }
}
