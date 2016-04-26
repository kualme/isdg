using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Isdg.Core.Data;
using Isdg.Models;
using Isdg.Services.Messages;

namespace Isdg.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmailController : Controller
    {
        private IEmailAccountService _emailAccountService;
        private IEmailSender _emailSender;

        public EmailController(IEmailAccountService emailAccountService, IEmailSender emailSender)
        {
            this._emailAccountService = emailAccountService;
            this._emailSender = emailSender;
        }

        public ActionResult Index()
        {
            return View(GetEmailAccounts());
        }

        public ActionResult CreateEditEmailAccount(EmailAccount model)        
        {
            if (model.Id == 0)
            {
                var currentDate = System.DateTime.Now;
                model.ModifiedDate = currentDate;
                model.AddedDate = currentDate;
                model.IP = Request.UserHostAddress;
                _emailAccountService.InsertEmailAccount(model);
            }
            else
            {
                var editModel = _emailAccountService.GetEmailAccountById(model.Id);
                editModel.DisplayName = model.DisplayName;
                editModel.Email = model.Email;
                editModel.EnableSsl = model.EnableSsl;
                editModel.Host = model.Host;
                editModel.Password = model.Password;
                editModel.Port = model.Port;
                editModel.UseDefaultCredentials = model.UseDefaultCredentials;
                editModel.Username = model.Username;
                editModel.ModifiedDate = System.DateTime.Now;
                editModel.IP = Request.UserHostAddress;
                _emailAccountService.UpdateEmailAccount(editModel);
            }
            return PartialView("_EmailAccountList", GetEmailAccounts()); 
        }

        public void SendTestEmail(TestEmailModel model)
        {
            var emailAccount = GetEmailAccounts().First();            
            _emailSender.SendEmail(emailAccount, "Test subject", "Test body", emailAccount.Email, emailAccount.DisplayName, model.Address, model.DisplayName);            
        }

        private IEnumerable<EmailAccount> GetEmailAccounts()
        {
            return _emailAccountService.GetAllEmailAccounts();
        }
    }    
}