using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Isdg.Core.Data
{
    public class News : BaseEntity
    {
        [AllowHtml]
        [Required(ErrorMessage = "Content is required"), DisplayFormat()]
        public string Content { get; set; }        
        [Display(Name="Published")]
        public bool IsPublished { get; set; }
        public string UserId { get; set; }
    }
}
