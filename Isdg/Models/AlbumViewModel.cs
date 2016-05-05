using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Isdg.Core.Data;

namespace Isdg.Models
{
    public class AlbumListViewModel 
    {
        public List<AlbumViewModel> Albums { get; set; }        
    }

    public class AlbumViewModel
    {
        public List<Image> News { get; set; }
        public bool CanDeleteNews { get; set; }
        public bool CanEditNews { get; set; }
        public bool CanSeeDetails { get; set; }
        public bool Show { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}