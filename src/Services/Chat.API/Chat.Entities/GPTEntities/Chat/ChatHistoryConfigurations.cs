using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entities.GPTEntities.Chat
{
    public class ChatHistoryConfigurations : IEntityTypeConfiguration<ChatHistory>
    {
        public void Configure(EntityTypeBuilder<ChatHistory> builder)
        {
            builder.HasKey(q => q.Id);

            //Removing this because all chatting history will be stored in RedisDB
            builder.Ignore(q => q.ChattingHistory);
        }
    }
}
