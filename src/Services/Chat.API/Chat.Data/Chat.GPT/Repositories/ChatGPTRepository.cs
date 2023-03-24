using Chat.Entities.GPTEntities.Chat;
using Chat.Exceptions.ChatGPTExceptions;
using Chat.GPT.Interfaces;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.GPT.Repositories
{
    public class ChatGPTRepository : IChatGPTRepository
    {
        public static OpenAIService ConfigureService(string apiKey, string? organization)
        {
            var openAiOptions = new OpenAiOptions()
            {
                ApiKey = apiKey,
                Organization = organization
            };

            openAiOptions.Validate();

            return new OpenAIService(openAiOptions);
        }

        public async Task<ChatHistory> SendMessage(ChatHistory userChatHistory, OpenAIService userOpenAIService)
        {
            var gptResponce = await userOpenAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest()
            {
                Messages = userChatHistory.ChattingHistory,
                Model = Models.ChatGpt3_5Turbo
            });

            if (gptResponce.Successful)
            {
                userChatHistory.ChattingHistory.Add(gptResponce.Choices.First().Message);
                return userChatHistory;
            }
            else
            {
                throw new ChatResponceException(gptResponce.Error?.Code ?? "Unknown", gptResponce.Error?.Message ?? "Unknown");
            }
        }
    }
}
