using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entities.EntityDepenceInterfaces
{
    public interface IDatabaseStorable
    {
        public string Id { get; set; }
    }
}
