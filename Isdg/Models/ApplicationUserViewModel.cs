using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Isdg.Models
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Display(Name = "Is email confirmed")]
        public bool EmailConfirmed { get; set; }
        [Display(Name = "Is newsletter received")]
        public bool ReceiveNewsletter { get; set; }
        [Display(Name = "User role")]
        public UserRole Role { get; set; }        
    }

    public enum UserRole
    {
        [Description("Unknown")]
        Unknown = 0,
        [Description("Untrusted member")]
        Untrusted = 1,
        [Description("Trusted member")]
        Trusted = 2,
        [Description("Administrator")]
        Admin = 3       
    }
}