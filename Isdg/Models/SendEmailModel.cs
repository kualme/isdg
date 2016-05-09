using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Isdg.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Isdg.Models
{    
    public class SendEmailModel
    {
        public string Subject { get; set; }
        public string Bidy { get; set; }
    }
}