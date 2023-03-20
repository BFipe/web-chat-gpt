using Chat.Entities.DatabaseEntities.GPTUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entities.GPTEntities.APIKey
{
    public class APIKey
    {
        public string APIKeyId { get; set; }
        public string APIKeyValue { get; set; }
        public DateTime LastUsed { get; set; }
        public string GPTUserId { get; set; }
        public GPTUser GPTUser { get; set; }

        public void RefreshUsingTracker()
        {
            LastUsed = DateTime.Now;
        }
    }
}
