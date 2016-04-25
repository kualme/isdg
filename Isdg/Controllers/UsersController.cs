using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Isdg.Models;
using Isdg.Services.Messages;
using Isdg.Core.Data;
using System.Configuration;
using System.Collections.ObjectModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Net;

namespace Isdg.Controllers
{    
    public class UsersController : Controller
    {
        private ApplicationUserManager userManager;
        private RoleManager<IdentityRole> roleManager;
        private ApplicationDbContext context;
        
        public UsersController()
        {
            context = new ApplicationDbContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }
        
        public ActionResult Index()
        {
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles;
            var model = new List<ApplicationUserViewModel>();
            foreach (var user in users)
            {                
                model.Add(new ApplicationUserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    UserName = user.UserName,
                    RoleList = MakeRoleList(user)
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationUserViewModel model, params string[] selectedRole) 
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = model.Email;
                user.Email = model.Email;
                user.EmailConfirmed = model.EmailConfirmed;
                                
                var userRoles = await userManager.GetRolesAsync(user.Id);
                selectedRole = selectedRole ?? new string[] { };
                var result = await userManager.AddToRolesAsync(user.Id,
                    selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await userManager.RemoveFromRolesAsync(user.Id,
                    userRoles.Except(selectedRole).ToArray<string>());
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return PartialView("_User");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        public async Task<ActionResult> ConfirmDeleteUser(string userId) 
        {
            var user = userManager.Users.SingleOrDefault(x => x.Id == userId);
            await userManager.DeleteAsync(user);            
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private List<SelectListItem> MakeRoleList(ApplicationUser user)
        {
            var roles = roleManager.Roles;
            var roleList = new List<SelectListItem>();
            foreach (var role in roles)
            {
                roleList.Add(new SelectListItem()
                {
                    Value = role.Id,
                    Selected = user.Roles.Any(x => x.RoleId == role.Id),
                    Text = role.Name
                });
            }
            return roleList;
        }
    }
}