using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isdg.Core.Data
{
    public class Text : BaseEntity
    {        
        public string Key { get; set; }
        public string Value { get; set; }
        public string UserId { get; set; }
    }
}
