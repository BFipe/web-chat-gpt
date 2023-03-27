using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Models.AccountModels
{
    public class AuthDto
    {
        public string Token { get; set; }
        public string UserEmail { get; set; }
        public string RefreshToken { get; set; }
    }
}
