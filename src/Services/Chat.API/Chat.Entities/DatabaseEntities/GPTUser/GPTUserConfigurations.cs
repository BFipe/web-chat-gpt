using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entities.DatabaseEntities.GPTUser
{
    public class GPTUserConfigurations : IEntityTypeConfiguration<GPTUser>
    {
        public void Configure(EntityTypeBuilder<GPTUser> builder)
        {
            builder.HasMany(q => q.APIKeys)
                .WithOne(q => q.GPTUser)
                .HasForeignKey(q => q.GPTUserId);

            builder.HasMany(q => q.ChatHistories)
                .WithOne(q => q.GPTUser)
                .HasForeignKey(q => q.GPTUserId);
        }
    }
}
