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
using Microsoft.AspNet.Identity;

namespace Isdg.Controllers
{
    [AuthorizeWithRoles(Role = UserRole.Admin)]
    public class NewsletterController : BaseController
    {        
        private IEmailSender emailSender;
        private ISentEmailService sendedEmailService;

        public NewsletterController(IEmailSender emailSender, ISentEmailService sendedEmailService)
        {            
            this.emailSender = emailSender;
            this.sendedEmailService = sendedEmailService;
        }

        public ActionResult Index()
        {
            var emails = sendedEmailService.GetAllEmails();            
            var model = emails.Select(x => new SentEmailViewModel() { Subject = x.Subject, Body = x.Body, UserName = UserHelper.GetUserName(UserManager), When = x.AddedDate });            
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
                        emailSender.SendEmail(account, model.Subject, model.Body, account.Email, account.DisplayName, user.Email, user.UsernameToDisplay);
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
                sendedEmailService.InsertEmail(sendedEmail);
                return PartialView("_SentEmail", new SentEmailViewModel() { Subject = model.Subject, Body = model.Body, When = currentDate, UserName = UserHelper.GetUserName(UserManager) });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to send email");                
                return PartialView("_SentEmail", null);
            }
        }        
    }    
}