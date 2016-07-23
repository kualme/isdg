using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Isdg.Core.Data;

namespace Isdg.Models
{
    public class CreateAwardViewModel
    {
        public CreateAwardViewModel()
        {
            Award = new Award();
        }
        public Award Award { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase FirstImageUpload { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase SecondImageUpload { get; set; }
        public string UserName { get; set; }
    }
}