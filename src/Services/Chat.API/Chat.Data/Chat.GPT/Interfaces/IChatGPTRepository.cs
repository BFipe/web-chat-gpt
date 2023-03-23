using Chat.Entities.GPTEntities.Chat;
using OpenAI.GPT3.Managers;

namespace Chat.GPT.Interfaces
{
    public interface IChatGPTRepository
    {
        Task<ChatHistory> SendMessage(ChatHistory userChatHistory, OpenAIService userOpenAIService);
    }
}