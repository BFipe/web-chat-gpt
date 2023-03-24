using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Exceptions.ChatGPTExceptions
{
    public class ChatResponceException : Exception
    {
        public ChatResponceException(string errorCode, string errorMessage) : base($"{errorCode} : {errorMessage}") { }
    }
}
