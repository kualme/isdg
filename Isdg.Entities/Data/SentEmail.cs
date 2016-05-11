using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isdg.Core.Data
{
    public class SentEmail : BaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
    }
}
