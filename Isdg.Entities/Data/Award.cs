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
    public class Award : BaseEntity
    {        
        [Required(ErrorMessage = "Heading is required"), DisplayFormat()]
        public string Heading { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Content is required"), DisplayFormat()]
        public string Content { get; set; }
        public string PathToFirstPicture { get; set; }
        public string PathToSecondPicture { get; set; }
        public string UserId { get; set; }
    }
}
