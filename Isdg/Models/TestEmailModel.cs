using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Isdg.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Isdg.Models
{    
    public class TestEmailModel
    {
        public string Address { get; set; }
        public string DisplayName { get; set; }
    }
}