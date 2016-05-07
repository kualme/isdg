using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Isdg.Core.Data;

namespace Isdg.Models
{
    public class MeetingListViewModel 
    {
        public List<MeetingViewModel> ComingMeetingsList { get; set; }
        public List<MeetingViewModel> PastMeetingsList { get; set; }
        public bool CanCreateMeeting { get; set; }
    }

    public class MeetingViewModel
    {
        public Meeting Meeting { get; set; }
        public bool CanDeleteMeetings { get; set; }
        public bool CanEditMeetings { get; set; }
        public bool CanSeeDetails { get; set; }
        public bool Show { get; set; }
        public string MeetingDate { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}