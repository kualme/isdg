using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Isdg.Models;
using Microsoft.AspNet.Identity;

namespace Isdg.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            var user = UserManager.FindById<ApplicationUser, string>(User.Identity.GetUserId());
            var username = user == null ? "username" : user.UsernameToDisplay;
            return PartialView("_LoginPartial", username);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Statutes()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult UsefulLinks()
        {
            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}