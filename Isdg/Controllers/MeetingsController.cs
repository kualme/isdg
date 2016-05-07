using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Isdg.Services.Information;

namespace Isdg.Controllers
{
    public class MeetingsController : BaseController
    {
        private readonly IMeetingService meetingService;

        public MeetingsController(IMeetingService meetingService) 
        {
            this.meetingService = meetingService;            
        }

        public ActionResult Related()
        {
            var meetings = meetingService.GetAllMeetings().Where(x => !x.IsIsdgMeeting);
            return View(meetings);
        }
    }
}