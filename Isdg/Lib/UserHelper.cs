using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Isdg.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

        public static string GetUserName(ApplicationUserManager manager, string userId = null)
        {
            if (userId == null) userId = HttpContext.Current.User.Identity.GetUserId();
            var user = manager.FindById<ApplicationUser, string>(userId);
            return user == null ? "username" : user.UsernameToDisplay;
        }
        
        public static List<string> GetAllAdminEmails(ApplicationUserManager manager)
        {
            var adminEmails = new List<string>();
            var users = manager.Users;            
            foreach (var user in users)
            {
                if (manager.IsInRole(user.Id, UserRole.Admin.ToString()))
                {
                    adminEmails.Add(user.Email);
                }                
            }
            return adminEmails;
        }

        public static List<string> GetAllEmailsToSendNewsletter(ApplicationUserManager manager)
        {
            var emails = new List<string>();
            var users = manager.Users;
            foreach (var user in users)
            {
                if (user.EmailConfirmed && user.ReceiveNewsletter)
                {
                    emails.Add(user.Email);
                }
            }
            return emails;
        }
    }
}