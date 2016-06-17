using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isdg.Core.Data
{
    public class ExecutiveBoardMember : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Display(Name = "Link")]
        public string Href { get; set; }
        [Required(ErrorMessage = "Start year is required")]
        [Display(Name = "Start year")]
        public string StartYear { get; set; }
        [Required(ErrorMessage = "End year is required")]
        [Display(Name = "End year")]
        public string Workplace { get; set; }
        public string Email { get; set; }
        public string EndYear { get; set; }
        [Display(Name = "Is former")]
        public bool IsFormer { get; set; }
        [Display(Name = "Is president")]
        public bool IsPresident { get; set; }
        [Display(Name = "Is dead")]
        public bool IsDead { get; set; }
        public string UserId { get; set; }
    }
}
