using Chat.Entities.DatabaseEntities.GPTUser;
using Chat.Entities.EntityDepenceInterfaces;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entities.GPTEntities.Chat
{
    public class ChatHistory : IDatabaseStorable
    {
        public string Id { get; set; }

        //ChatHistory property will be ignored for EFCore migration
        //because all Chat history will be stored into RedisDB by their Id
        public List<ChatMessage> ChattingHistory { get; set; }
        public string GPTUserId { get; set; }
        public GPTUser GPTUser { get; set; }
    }
}
