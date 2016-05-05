using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Isdg.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Isdg.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationUserManager userManager;
        private RoleManager<IdentityRole> roleManager;
        private ApplicationDbContext context;

        public ApplicationUserManager UserManager { get { return userManager; } }
        public RoleManager<IdentityRole> RoleManager { get { return roleManager; } }

        public BaseController()
        {
            context = new ApplicationDbContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }
    }
}