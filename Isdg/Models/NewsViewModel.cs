using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Isdg.Core.Data;

namespace Isdg.Models
{
    public class NewsListViewModel 
    {
        public List<NewsViewModel> NewsList { get; set; }
        public bool CanCreateNews { get; set; }
    }

    public class NewsViewModel
    {
        public News News { get; set; }
        public bool CanDeleteNews { get; set; }
        public bool CanEditNews { get; set; }
        public bool CanSeeDetails { get; set; }
        public bool Show { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}