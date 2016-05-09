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
        private ISendedEmailService sendedEmailService;

        public NewsletterController(IEmailSender emailSender, ISendedEmailService sendedEmailService)
        {            
            this.emailSender = emailSender;
            this.sendedEmailService = sendedEmailService;
        }

        public ActionResult Index()
        {
            var emails = sendedEmailService.GetAllEmails();
            var user = UserManager.FindById<ApplicationUser, string>(User.Identity.GetUserId());
            var username = user == null ? "username" : user.UsernameToDisplay;
            var model = emails.Select(x => new SendedEmailViewModel() { Subject = x.Subject, Body = x.Body, UserName = username, When = x.AddedDate });            
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
                var sendedEmail = new SendedEmail() { 
                    Subject = model.Subject, 
                    Body = model.Body, 
                    UserId = User.Identity.GetUserId(),
                    AddedDate = currentDate,
                    ModifiedDate = currentDate,
                    IP = Request.UserHostAddress
                };
                sendedEmailService.InsertEmail(sendedEmail);
                return PartialView("_SendedEmail", new SendedEmailViewModel() { Subject = model.Subject, Body = model.Body, When = currentDate, UserName = UserManager.FindById<ApplicationUser, string>(User.Identity.GetUserId()).UsernameToDisplay });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to send email");                
                return PartialView("_SendedEmail", null);
            }
        }        
    }    
}