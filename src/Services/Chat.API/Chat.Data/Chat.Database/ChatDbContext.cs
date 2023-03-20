using Chat.Entities.DatabaseEntities.GPTUser;
using Chat.Entities.GPTEntities.APIKey;
using Chat.Entities.GPTEntities.Chat;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Database
{
    public class ChatDbContext : IdentityDbContext<GPTUser>
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {

        }
        public DbSet<GPTUser> GPTUsers { get; set; }
        public DbSet<APIKey> APIKeys { get; set; }
        public DbSet<ChatHistory> ChatHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new GPTUserConfigurations());
            builder.ApplyConfiguration(new APIKeyConfigurations());
            builder.ApplyConfiguration(new ChatHistoryConfigurations());
        }
    }
}
