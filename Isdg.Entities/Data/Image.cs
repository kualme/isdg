﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isdg.Core.Data
{
    public class Image : BaseEntity
    {
        public string Path { get; set; }
        public string PathToPreview { get; set; }
        public string Caption { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Published")]
        public bool IsPublished { get; set; }
        public virtual Album Album { get; set; }        
    }
}
