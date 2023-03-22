using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entities.GPTEntities.APIKey
{
    public class APIKeyConfigurations : IEntityTypeConfiguration<APIKey>
    {
        public void Configure(EntityTypeBuilder<APIKey> builder)
        {
            builder.HasKey(q => q.Id);

            builder.HasIndex(q => q.APIKeyValue).IsUnique(true);
        }
    }
}
