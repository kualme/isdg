using Isdg.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Isdg.Core.Data
{
    public class Meeting : BaseEntity
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Place is required")]
        public string Place { get; set; }
        [Required(ErrorMessage = "Link is required")]
        [Display(Name = "Link")]
        public string Href { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }        
        [Display(Name = "Meeting type")]
        public MeetingType MeetingType { get; set; }        
        public bool IsIsdgMeeting { get; set; }
        [Display(Name = "Published")]
        public bool IsPublished { get; set; }
        public string UserId { get; set; }
    }

    public enum MeetingType
    {
        Unknown = 0,
        Conference = 1,
        Workshop = 2
    }
}
