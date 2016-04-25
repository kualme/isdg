using Isdg.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isdg.Core.Data
{
    public class Meeting : BaseEntity
    {
        public string Title { get; set; }
        public string Place { get; set; }
        public DateTime StartDate { get; set; }
        public string EndDate { get; set; }
        public MeetingType MeetingType { get; set; }
        public bool IsIsdgMeeting { get; set; }
        public bool IsPublished { get; set; }
    }

    public enum MeetingType
    {
        Unknown = 0,
        Conference = 1,
        Workshop = 2
    }
}
