using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Isdg.Models;

namespace Isdg.Lib
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeWithRolesAttribute : AuthorizeAttribute
    {
        //public UserRole[] RoleList { get; set; }

        public UserRole Role { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }
            //if ((RoleList.Length > 0) && !RoleList.Select(p=>p.ToString()).Any<string>(new Func<string, bool>(user.IsInRole)))
            //{
            //    return false;
            //}
            if (!user.IsInRole(Role.ToString()))
            {
                return false;
            }
            return true;
        }
    }
}