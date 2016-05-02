using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isdg.Core.Data
{
    public class News : BaseEntity
    {        
        [Required(ErrorMessage = "Content is required"), DisplayFormat()]
        public string Content { get; set; }        
        [Display(Name="Is published")]
        public bool IsPublished { get; set; }
        public string UserId { get; set; }
    }
}
