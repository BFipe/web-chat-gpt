using Chat.Entities.GPTEntities.Chat;
using Chat.Redis.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Redis.Repositories
{
    public class RedisDatabaseRepository : IRedisDatabaseRepository
    {
        private readonly IDistributedCache _redis;
        private readonly ILogger<RedisDatabaseRepository> _logger;

        public RedisDatabaseRepository(IDistributedCache redis, ILogger<RedisDatabaseRepository> logger)
        {
            _redis = redis;
            _logger = logger;
        }

        public async Task<ChatHistory> GetChatHistoryByIdAsync(string id)
        {
            _logger.LogInformation($"Searching chat with id {id}");

            var result = await _redis.GetStringAsync(id);

            if (String.IsNullOrEmpty(result))
            {
                _logger.LogWarning($"Chat with id {id} is not found");
                return null;
            }

            _logger.LogInformation($"Sucessfully found chat with id {id}");

            var chatHistory = JsonConvert.DeserializeObject<ChatHistory>(result);

            return chatHistory;
        }

        public async Task<ChatHistory> UpdateChatHistoryAsync(ChatHistory chatHistory)
        {
            _logger.LogInformation($"Updating chat with id {chatHistory.Id}");

            var serialisedChatHistory = JsonConvert.SerializeObject(chatHistory);

            await _redis.SetStringAsync(chatHistory.Id, serialisedChatHistory);

            _logger.LogInformation($"Sucessfully updated chat with id {chatHistory.Id}");

            return await GetChatHistoryByIdAsync(chatHistory.Id);
        }

        public async Task DeleteChatByIdAsync(string id)
        {
            _logger.LogInformation($"Deleting chat with id {id}");

            await _redis.RemoveAsync(id);

            _logger.LogInformation($"Sucessfully deleted chat with id {id}");
        }
    }
}
