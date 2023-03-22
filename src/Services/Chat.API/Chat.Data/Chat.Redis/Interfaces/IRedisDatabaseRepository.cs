using Chat.Entities.GPTEntities.Chat;

namespace Chat.Redis.Interfaces
{
    public interface IRedisDatabaseRepository
    {
        Task DeleteChatByIdAsync(string id);
        Task<ChatHistory> GetChatHistoryByIdAsync(string id);
        Task<ChatHistory> UpdateChatHistoryAsync(ChatHistory chatHistory);
    }
}