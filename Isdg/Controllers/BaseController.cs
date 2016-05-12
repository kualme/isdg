using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Isdg.Models;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Isdg.Controllers
{
    public class BaseController : Controller
    {
        private ILog log;
        private ApplicationUserManager userManager;
        private RoleManager<IdentityRole> roleManager;
        private ApplicationDbContext context;

        public ApplicationUserManager UserManager { get { return userManager; } }
        public RoleManager<IdentityRole> RoleManager { get { return roleManager; } }
        public ILog Log { get { return log; } }

        public BaseController()
        {
            context = new ApplicationDbContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));            
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);            
        }
    }
}