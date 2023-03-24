using Chat.Database.Interfaces;
using Chat.Entities.GPTEntities.Chat;
using Chat.Redis.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Services
{
    public class ChatHistoryService
    {
        private readonly IRedisDatabaseRepository _redisDatabaseRepository;
        private readonly IGPTBaseRepository<ChatHistory> _ChatHistoryDatabase;
        private readonly ILogger<ChatHistoryService> _logger;

        public ChatHistoryService(IRedisDatabaseRepository redisDatabaseRepository,
                                  IGPTBaseRepository<ChatHistory> chatHistoryDatabase,
                                  ILogger<ChatHistoryService> logger)
        {
            _redisDatabaseRepository = redisDatabaseRepository;
            _ChatHistoryDatabase = chatHistoryDatabase;
            _logger = logger;
        }

        
    }
}
