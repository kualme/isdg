using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Isdg.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Isdg.Models
{    
    public class SendEmailModel
    {
        [Required]
        public string Subject { get; set; }
        [AllowHtml]
        [Required]
        public string Body { get; set; }
    }

    public class SentEmailViewModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string UserName { get; set; }
        public DateTime When { get; set; }
    }
}