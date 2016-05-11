using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Isdg.Models;
using Microsoft.AspNet.Identity;

namespace Isdg.Lib
{
    public static class UserHelper
    {
        public static bool IsAdmin()
        {
            return HttpContext.Current.User.IsInRole(UserRole.Admin.ToString());
        }

        public static bool IsTrusted()
        {
            return HttpContext.Current.User.IsInRole(UserRole.Trusted.ToString());
        }

        public static bool IsAdminOrTrusted()
        {
            return HttpContext.Current.User.IsInRole(UserRole.Admin.ToString()) || HttpContext.Current.User.IsInRole(UserRole.Trusted.ToString());
        }

        public static bool HasAnyRole()
        {
            return HttpContext.Current.User.IsInRole(UserRole.Admin.ToString()) || HttpContext.Current.User.IsInRole(UserRole.Trusted.ToString()) || HttpContext.Current.User.IsInRole(UserRole.Untrusted.ToString());
        }

        public static string GetUserName(ApplicationUserManager manager)
        {
            var user = manager.FindById<ApplicationUser, string>(HttpContext.Current.User.Identity.GetUserId());
            return user == null ? "username" : user.UsernameToDisplay;
        }
    }
}