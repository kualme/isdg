using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isdg.Core.Data
{
    public class Album : BaseEntity
    {
        public Album()
        {
            var Images = new List<Image>();
        }
        public string Name { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
