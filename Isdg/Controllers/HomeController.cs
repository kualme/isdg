using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Isdg.Core.Data;
using Isdg.Lib;
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
            return PartialView("_LoginPartial", UserHelper.GetUserName(UserManager));
        }

        public ActionResult ShowText(string key)
        {
            ViewBag.Title = key;
            var text = textService.GetTextByKey(key);
            if (text == null)
                return View("Create", new TextViewModel() { Key = key });            
            return View("Text", new TextViewModel() { Key = key, Content = text.Value, UserName = UserHelper.GetUserName(UserManager) });
        }
                
        public ActionResult ExecutiveBoard()
        {
            return View();
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
                return ShowText(model.Key);
            }
            return View();            
        }
    }
}