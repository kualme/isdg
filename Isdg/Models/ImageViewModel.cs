using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Isdg.Core.Data;

namespace Isdg.Models
{
    public class ImageViewModel
    {
        public Image Image { get; set; }
        public bool CanEditImage { get; set; }
        public bool CanDeleteImage { get; set; }
        public bool Show { get; set; }
    }
}