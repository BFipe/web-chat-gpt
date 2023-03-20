using Chat.Entities.GPTEntities.APIKey;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entities.DatabaseEntities.GPTUser
{
    public class GPTUser : IdentityUser
    {
        public List<APIKey> APIKeys { get; set; }
    }
}
