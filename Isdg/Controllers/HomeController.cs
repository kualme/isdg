using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Isdg.Core.Data;
using Isdg.Models;
using Isdg.Services.Information;
using Microsoft.AspNet.Identity;

namespace Isdg.Controllers
{
    public class HomeController : BaseController
    {
        private ITextService textService;

        public HomeController(ITextService textService)
        {
            this.textService = textService;
        }

        public ActionResult Login()
        {
            var user = UserManager.FindById<ApplicationUser, string>(User.Identity.GetUserId());
            var username = user == null ? "username" : user.UsernameToDisplay;
            return PartialView("_LoginPartial", username);
        }

        public ActionResult AboutIsdg()
        {
            return ProcessTexts("About ISDG", "AboutIsdg");            
        }

        public ActionResult StatutesOfIsdg()
        {
            return ProcessTexts("Statutes of ISDG", "StatutesOfIsdg");            
        }

        public ActionResult ExecutiveBoard()
        {
            return ProcessTexts("Executive Board", "ExecutiveBoard");
        }

        public ActionResult IsdgMeetings()
        {
            return ProcessTexts("ISDG Meetings", "IsdgMeetings");
        }

        public ActionResult Publications()
        {
            return ProcessTexts("Publications", "Publications");
        }

        public ActionResult UsefulLinks()
        {
            return ProcessTexts("Useful Links", "UsefulLinks");
        }

        public ActionResult IsaacsAward()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateText(TextViewModel model)
        {
            var currentDate = DateTime.Now;
            var text = new Text() 
            {
                Key = model.Key,
                Value = model.Content,
                UserId = User.Identity.GetUserId(),
                AddedDate = currentDate,
                ModifiedDate = currentDate,
                IP = Request.UserHostAddress
            };
            textService.InsertText(text);
            return RedirectToAction(model.Key);
        }
                
        public ActionResult EditText(string key)
        {
            var text = textService.GetTextByKey(key);
            if (text != null)
            {
                var model = new TextViewModel()
                {
                    Key = key,
                    Content = text.Value
                };
                return View("Edit", model);
            }            
            return View();
        }

        [HttpPost]
        public ActionResult EditText(TextViewModel model)
        {
            var text = textService.GetTextByKey(model.Key);
            if (text != null)
            {
                text.Value = model.Content;
                textService.UpdateText(text);
                return RedirectToAction(model.Key);
            }
            return View();            
        }

        private ActionResult ProcessTexts(string title, string key)
        {
            ViewBag.Title = title;
            var text = textService.GetTextByKey(key);
            if (text == null)
                return View("Create", new TextViewModel() { Key = key });
            var user = UserManager.FindById<ApplicationUser, string>(User.Identity.GetUserId());
            var username = user == null ? "username" : user.UsernameToDisplay;
            return View("Content", new TextViewModel() { Key = key, Content = text.Value, UserName = username });
        }
    }
}