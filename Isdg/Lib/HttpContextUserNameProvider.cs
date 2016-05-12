using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Isdg.Lib
{
    public class HttpContextUserNameProvider
    {
        public override string ToString()
        {
            HttpContext context = HttpContext.Current;
            if (context != null && context.User != null && context.User.Identity.IsAuthenticated)
            {
                return context.User.Identity.Name;
            }
            return "";
        }
    }
}