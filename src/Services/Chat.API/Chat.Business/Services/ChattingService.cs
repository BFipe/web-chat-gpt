using AutoMapper;
using Chat.Business.Interfaces;
using Chat.Database.Interfaces;
using Chat.Entities.DatabaseEntities.GPTUser;
using Chat.Entities.GPTEntities.APIKey;
using Chat.Entities.GPTEntities.Chat;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Services
{
    public class ChattingService : IChattingService
    {
        private readonly IGPTBaseRepository<GPTUser> _gptUserRepository;
        private readonly IGPTBaseRepository<APIKey> _gptAPIRepository;
        private readonly IGPTBaseRepository<ChatHistory> _gptChatHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ChattingService> _logger;

        public ChattingService(IGPTBaseRepository<GPTUser> gptUserRepository,
                               IGPTBaseRepository<APIKey> gptAPIRepository,
                               IGPTBaseRepository<ChatHistory> gptChatHistoryRepository,
                               IMapper mapper,
                               ILogger<ChattingService> logger)
        {
            _gptUserRepository = gptUserRepository ?? throw new ArgumentNullException(nameof(gptUserRepository));
            _gptAPIRepository = gptAPIRepository ?? throw new ArgumentNullException(nameof(gptAPIRepository));
            _gptChatHistoryRepository = gptChatHistoryRepository ?? throw new ArgumentNullException(nameof(gptChatHistoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task Test()
        {
            var data = await _gptUserRepository.GetAsync(q => q.Email == "user@example.com");

            _logger.LogDebug($"Responce from db - got {data.Count()} rows of data");

            foreach (var item in data)
            {
                _logger.LogDebug(item.NormalizedUserName);
            }
        }
    }
}
