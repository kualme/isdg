using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Isdg.Core.Data;
using Isdg.Lib;
using Isdg.Models;
using Isdg.Services.Messages;

namespace Isdg.Controllers
{
    [AuthorizeWithRoles(Role = UserRole.Admin)]
    public class NewsletterController : Controller
    {        
        private IEmailSender _emailSender;

        public NewsletterController(IEmailSender emailSender)
        {            
            this._emailSender = emailSender;
        }

        public ActionResult Index()
        {
            return View();
        }        

        public void SendEmail(SendEmailModel model)
        {            
            //_emailSender.SendEmail(, "Test subject", "Test body", emailAccount.Email, emailAccount.DisplayName, model.Address, model.DisplayName);            
        }
    }    
}