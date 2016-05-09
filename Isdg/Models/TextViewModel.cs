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
    public class TextViewModel
    {
        [Required]
        public string Key { get; set; }
        [AllowHtml]
        [Required]
        public string Content { get; set; }
        public string UserName { get; set; }
    }    
}