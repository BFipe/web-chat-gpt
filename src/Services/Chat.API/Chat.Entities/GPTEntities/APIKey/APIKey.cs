using Chat.Entities.DatabaseEntities.GPTUser;
using Chat.Entities.EntityDepenceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entities.GPTEntities.APIKey
{
    public class APIKey : IDatabaseStorable
    {
        public string Id { get; set; }
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
