using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Isdg.Core;
using Isdg.Core.Data;
using Isdg.Lib;
using Isdg.Models;
using Isdg.Services.Information;
using Isdg.Services.Messages;
using log4net;
using Microsoft.AspNet.Identity;

namespace Isdg.Controllers
{
    [AuthorizeWithRoles(Role = UserRole.Admin)]
    public class NewsletterController : BaseController
    {   
        private ISentEmailService sentEmailService;

        public NewsletterController(IEmailSender emailSender, ISentEmailService sendedEmailService, ILog log) : base(log, emailSender)
        {   
            this.sentEmailService = sendedEmailService;
        }

        public ActionResult Index()
        {
            var emails = sentEmailService.GetAllEmails();            
            var model = emails.Select(x => new SentEmailViewModel() { Subject = x.Subject, Body = x.Body, UserName = UserHelper.GetUserName(UserManager, x.UserId), When = x.AddedDate });            
            return View(model);
        }
        
        [HttpPost]
        public ActionResult SendEmail(SendEmailModel model)
        {   
            var account = Settings.EmailAccount;
            var users = UserManager.Users;
            try
            {
                foreach (var user in users)
                {
                    if (user.ReceiveNewsletter && user.EmailConfirmed)
                        EmailSender.SendEmail(account, model.Subject, model.Body, account.Email, account.DisplayName, user.Email, user.UsernameToDisplay);
                }
                var currentDate = DateTime.Now;
                var sendedEmail = new SentEmail() { 
                    Subject = model.Subject, 
                    Body = model.Body, 
                    UserId = User.Identity.GetUserId(),
                    AddedDate = currentDate,
                    ModifiedDate = currentDate,
                    IP = Request.UserHostAddress
                };
                sentEmailService.InsertEmail(sendedEmail);
                return PartialView("_SentEmail", new SentEmailViewModel() { Subject = model.Subject, Body = model.Body, When = currentDate, UserName = UserHelper.GetUserName(UserManager) });
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", "Failed to send email");                
                return PartialView("_SentEmail", null);
            }
        }        
    }    
}