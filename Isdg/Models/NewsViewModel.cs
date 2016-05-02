using System;
using System.Collections.Generic;
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
    }
}