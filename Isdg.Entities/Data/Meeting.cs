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
        public string Title { get; set; }
        public string Place { get; set; }
        [Display(Name = "Link")]
        public string Href { get; set; }
        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }        
        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
