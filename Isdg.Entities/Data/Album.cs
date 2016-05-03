using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isdg.Core.Data
{
    public class Album : BaseEntity
    {
        public string Name { get; set; }
        public List<Image> Images { get; set; }
    }
}
