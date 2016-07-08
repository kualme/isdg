using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Isdg.Services.Information;
using Isdg.Core.Data;
using Isdg.Data;
using Ninject;
using Isdg.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Isdg.Lib;
using log4net;
using Isdg.Services.Messages;
using System.IO;

namespace Isdg.Controllers
{    
    public class AwardController : BaseController
    {
        private readonly IAwardService awardService;

        public AwardController(IAwardService awardService, ILog log, IEmailSender emailSender)
            : base(log, emailSender) 
        {
            this.awardService = awardService;            
        }
                
        public ActionResult Index()
        {
            var awards = awardService.GetAllAwards();
            return View(awards);
        }
        
        [HttpPost]
        public ActionResult CreateEditAward(CreateAwardViewModel model)
        {
            if (model.Award.Id == 0)
            {                
                var currentDate = System.DateTime.Now;
                model.Award.ModifiedDate = currentDate;
                model.Award.AddedDate = currentDate;
                model.Award.IP = Request.UserHostAddress;

                if (Request.Files.Count > 0)
                {
                    var path = SaveImage(Request.Files[0]);
                    model.Award.PathToFirstPicture = path;
                }

                if (Request.Files.Count > 1)
                {
                    var secondPath = SaveImage(Request.Files[1]);
                    model.Award.PathToFirstPicture = secondPath;
                }

                try
                {
                    model.Award.UserId = User.Identity.GetUserId();
                    awardService.InsertAward(model.Award);                    
                    return PartialView("_Award", model);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    ModelState.AddModelError("", "Failed to create award");
                    return PartialView("_Award", null);
                }                
            }
            else
            {
                try
                {
                    var editModel = awardService.GetAwardById(model.Award.Id);
                    editModel.Heading = model.Award.Heading;
                    editModel.Content = model.Award.Content;                    
                    editModel.ModifiedDate = System.DateTime.Now;
                    editModel.IP = Request.UserHostAddress;
                    
                    if (Request.Files.Count > 0)
                    {
                        var path = SaveImage(Request.Files[0]);
                        editModel.PathToFirstPicture = path;
                    }

                    if (Request.Files.Count > 1)
                    {
                        var secondPath = SaveImage(Request.Files[1]);
                        editModel.PathToFirstPicture = secondPath;
                    }
                    
                    awardService.UpdateAward(editModel);                    
                    return PartialView("_Award", editModel);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    ModelState.AddModelError("", "Failed to update award");
                    return PartialView("_Award", model);
                }                
            }                     
        }

        [HttpPost]
        public ActionResult ConfirmDeleteAward(int awardId)
        {
            try
            {
                var model = awardService.GetAwardById(awardId);            
                awardService.DeleteAward(model);                
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", "Failed to delete award");
                return PartialView("_Award", null);
            }            
        }
        
        private string SaveImage(HttpPostedFileBase file)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileGuid = Guid.NewGuid();
            var fileName = fileGuid + extension;
            var path = Path.Combine(Server.MapPath("~/Content/Award/"), fileName);
            file.SaveAs(path);
            return path;
        }
    }
}
