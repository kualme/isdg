using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Isdg.Core.Data;

namespace Isdg.Models
{
    public class NewsViewModel
    {
        public News News { get; set; }
        public bool CanDeleteNews { get; set; }
        public bool CanEditNews { get; set; }
        public bool CanCreateNews { get; set; }
    }
}