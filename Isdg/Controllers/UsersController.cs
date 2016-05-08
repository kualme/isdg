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
using System.Web.Http;
using Isdg.Lib;

namespace Isdg.Controllers
{
    [AuthorizeWithRoles(Role = UserRole.Admin)]
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
                model.Add(ToApplicationUserViewModel(user));
            }
            return View(model);
        }
                
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser([FromBody]ApplicationUserViewModel model, bool redirectToIndex = false)
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

                var roles = new string[] { model.Role.ToString() };
                var userRoles = await userManager.GetRolesAsync(user.Id);                
                var result = await userManager.AddToRolesAsync(user.Id,
                    roles.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    return HandleError(model, result, redirectToIndex);   
                }

                result = await userManager.RemoveFromRolesAsync(user.Id,
                    userRoles.Except(roles).ToArray<string>());
                if (!result.Succeeded)
                {
                    return HandleError(model, result, redirectToIndex);   
                }

                return PartialView("_User", model);
            }
            return HandleError(model, null, redirectToIndex);   
        }

        public async Task<ActionResult> ConfirmDeleteUser(string userId) 
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            return HandleError(ToApplicationUserViewModel(user), result);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }            
            return View(ToApplicationUserViewModel(user));
        }
        
        public async Task<ActionResult> Details(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(ToApplicationUserViewModel(user));
        }

        private ApplicationUserViewModel ToApplicationUserViewModel(ApplicationUser user)
        {
            return new ApplicationUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                UserName = user.UserName,
                Role = GetRole(user)
            };
        }

        private UserRole GetRole(ApplicationUser user)
        {
            var roles = roleManager.Roles;
            var userRole = UserRole.Untrusted;
            foreach (var role in roles)
            {
                if (user.Roles.Any(x => x.RoleId == role.Id))
                    Enum.TryParse<UserRole>(role.Name, out userRole);
            }
            return userRole;
        }

        private ActionResult HandleError(ApplicationUserViewModel model, IdentityResult result = null, bool redirectToIndex = false)
        {
            if (result == null)
                ModelState.AddModelError("", "Something failed");
            else ModelState.AddModelError("", result.Errors.First());
            if (redirectToIndex)
                return RedirectToAction("Index");
            return PartialView("_User", model);
        }
    }
}