using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Isdg.Core.Data;
using Isdg.Lib;
using Isdg.Models;
using Isdg.Services.Information;
using Isdg.Services.Messages;
using log4net;
using Microsoft.AspNet.Identity;

namespace Isdg.Controllers
{
    public class MeetingsController : BaseController
    {
        private readonly IMeetingService meetingService;

        public MeetingsController(IMeetingService meetingService, ILog log, IEmailSender emailSender) : base(log, emailSender)
        {
            this.meetingService = meetingService;            
        }
        
        public ActionResult Related()
        {
            var meetings = meetingService.GetAllMeetings(0, int.MaxValue, MeetingType.Unknown, false);
            return View(ToMeetingListViewModel(meetings));
        }

        [HttpPost]
        public ActionResult CreateEditMeeting(Meeting model)
        {
            if (model.Id == 0)
            {
                var currentDate = System.DateTime.Now;
                model.ModifiedDate = currentDate;
                model.AddedDate = currentDate;
                model.IP = Request.UserHostAddress;                
                if (UserHelper.IsAdmin())
                    model.IsPublished = true;
                try
                {
                    model.UserId = User.Identity.GetUserId();
                    meetingService.InsertMeeting(model);
                    if (!UserHelper.IsAdmin())
                    {
                        EmailSender.SendEmailOnCreate(UserHelper.GetAllAdminEmails(UserManager), "related meeting", Url.Action("Details", "Meetings", new { id = model.Id }, Request.Url.Scheme), User.Identity.GetUserName());
                    }
                    return PartialView("_Meeting", ToMeetingViewModel(model));
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    ModelState.AddModelError("", "Failed to create meeting");
                    return PartialView("_Meeting", null);
                }
            }
            else
            {
                try
                {
                    var editModel = meetingService.GetMeetingById(model.Id);
                    editModel.Title = model.Title;
                    editModel.Place = model.Place;
                    editModel.StartDate = model.StartDate;
                    editModel.EndDate = model.EndDate;
                    editModel.IsPublished = model.IsPublished;
                    editModel.IsIsdgMeeting = model.IsIsdgMeeting;
                    editModel.MeetingType = model.MeetingType;
                    editModel.ModifiedDate = System.DateTime.Now;
                    editModel.IP = Request.UserHostAddress;
                    meetingService.UpdateMeeting(editModel);
                    return PartialView("_Meeting", ToMeetingViewModel(editModel));
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    ModelState.AddModelError("", "Failed to update meeting");
                    return PartialView("_Meeting", model);
                }
            }
        }

        [HttpPost]
        public ActionResult ConfirmDeleteMeeting(int meetingId)
        {
            try
            {
                var model = meetingService.GetMeetingById(meetingId);
                meetingService.DeleteMeeting(model);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", "Failed to delete meeting");
                return PartialView("_Meeting", null);
            }
        }

        [AuthorizeWithRoles(Role = UserRole.Admin)]
        public ActionResult Edit(int? id)
        {
            var model = new Meeting();
            if (id.HasValue)
            {
                model = meetingService.GetMeetingById(id.Value);
            }
            return View(model);
        }

        [HttpPost]
        [AuthorizeWithRoles(Role = UserRole.Admin)]
        public ActionResult EditMeeting(Meeting model)
        {
            try
            {
                var editModel = meetingService.GetMeetingById(model.Id);
                editModel.Title = model.Title;
                editModel.Place = model.Place;
                editModel.StartDate = model.StartDate;
                editModel.EndDate = model.EndDate;
                editModel.IsPublished = model.IsPublished;
                editModel.IsIsdgMeeting = model.IsIsdgMeeting;
                editModel.MeetingType = model.MeetingType;
                editModel.ModifiedDate = System.DateTime.Now;
                editModel.IP = Request.UserHostAddress;
                meetingService.UpdateMeeting(editModel);
                return RedirectToAction("Related");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", "Failed to edit meeting");
                return View("Edit", model);
            }
        }

        [AuthorizeWithRoles(Role = UserRole.Admin)]
        public ActionResult Details(int id)
        {
            var model = meetingService.GetMeetingById(id);
            return View(model);
        }

        private MeetingViewModel ToMeetingViewModel(Meeting meeting)
        {
            var model = new MeetingViewModel() { Meeting = meeting, Show = meeting.IsPublished};            
            if (meeting.StartDate == meeting.EndDate)
                model.MeetingDate = meeting.StartDate.ToString("MMMM d, yyyy");
            else if (meeting.StartDate.Year == meeting.EndDate.Year && meeting.StartDate.Month == meeting.EndDate.Month)
                model.MeetingDate = String.Format("{0} {1}-{2}, {3}", meeting.StartDate.ToString("MMMM"), meeting.StartDate.Day, meeting.EndDate.Day, meeting.StartDate.Year);
            else if (meeting.StartDate.Year == meeting.EndDate.Year)
                model.MeetingDate = String.Format("{0} {1}-{2} {3}, {4}", meeting.StartDate.ToString("MMMM"), meeting.StartDate.Day, meeting.EndDate.ToString("MMMM"), meeting.EndDate.Day, meeting.StartDate.Year);
            else model.MeetingDate = String.Format("{0}-{1}", meeting.StartDate.ToString("MMMM d, yyyy"), meeting.EndDate.ToString("MMMM d, yyyy"));
            var user = UserManager.Users.FirstOrDefault(x => x.Id == meeting.UserId);
            model.UserName = UserHelper.GetUserName(UserManager, meeting.UserId);
            if (UserHelper.IsAdmin())
            {
                model.CanDeleteMeetings = true;
                model.CanEditMeetings = true;
                model.CanSeeDetails = true;
                model.Show = true;                
            }
            else if (UserHelper.IsTrusted())
            {                
                if (User.Identity.GetUserId().Equals(meeting.UserId))
                {
                    model.CanEditMeetings = true;
                    model.Show = true;
                }                
            }

            return model;
        }

        private MeetingListViewModel ToMeetingListViewModel(IEnumerable<Meeting> meeting)
        {
            var model = new MeetingListViewModel();
            model.CanCreateMeeting = UserHelper.HasAnyRole();
            model.ComingMeetingsList = meeting.Where(x => x.EndDate >= DateTime.Now).Select(ToMeetingViewModel).ToList();
            var pastMeetings = meeting.Where(x => x.EndDate < DateTime.Now).Select(ToMeetingViewModel).ToList();
            foreach (var pastMeeting in pastMeetings)
            {
                var year = pastMeeting.Meeting.StartDate.Year;
                var key = year.ToString();
                if (year < 2010)
                    key = "Earlier...";
                if (model.PastMeetingsDict.ContainsKey(key))
                    model.PastMeetingsDict[key].Add(pastMeeting);
                else 
                {
                    model.PastMeetingsDict.Add(key, new List<MeetingViewModel>() { pastMeeting });
                }
            }
            return model;
        }
    }        
}