using Chat.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChattingService _chattingService;

        public ChatController(IChattingService chattingService)
        {
            _chattingService = chattingService ?? throw new ArgumentNullException(nameof(chattingService));
        }

        [HttpGet]
        [Route("GetAllChats")]
        public async Task<IActionResult> GetAllUserChats()
        {
            await _chattingService.Test();
            return Ok();
        }

        [HttpGet]
        [Route("GetChat")]
        public async Task<IActionResult> GetChatById(string id)
        {
            //This method returns one chat by its id
            return Ok();
        }

        [HttpGet]
        [Route("CreateChat")]
        public async Task<IActionResult> CreateNewChat()
        {
            //This method returns new generated chat
            return Ok();
        }

        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage(string chatId, string messageContent)
        {
            //This method sends user message to specified chat
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteChat")]
        public async Task<IActionResult> DeleteChat(string id)
        {
            //This method returns all logged-in user chats
            return Ok();
        }
    }
}
