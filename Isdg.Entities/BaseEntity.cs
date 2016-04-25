using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isdg.Core
{
    public class BaseEntity
    {
        [Display(Name = "Identifier")]
        public int Id { get; set; }
        [Display(Name = "Added date")]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        public DateTime AddedDate { get; set; }
        [Display(Name = "Modified date")]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        public DateTime ModifiedDate { get; set; }
        [Display(Name = "IP address")]
        public string IP { get; set; }
    }
}
