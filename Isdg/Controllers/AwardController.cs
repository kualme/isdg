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
            return View(awards.Select(ToAwardViewModel));
        }
        
        [HttpPost]
        public ActionResult CreateEditAward(Award model)
        {
            if (model.Id == 0)
            {                
                var currentDate = System.DateTime.Now;
                model.ModifiedDate = currentDate;
                model.AddedDate = currentDate;
                model.IP = Request.UserHostAddress;

                if (Request.Files.Count > 0)
                {
                    var path = SaveImage(Request.Files[0]);
                    model.PathToFirstPicture = path;
                }

                if (Request.Files.Count > 1)
                {
                    var secondPath = SaveImage(Request.Files[1]);
                    model.PathToSecondPicture = secondPath;
                }

                try
                {
                    model.UserId = User.Identity.GetUserId();
                    awardService.InsertAward(model);
                    return new JsonResult() { Data = new { success = true } };
                    //return RedirectToAction("Index");
                    //return PartialView("_Award", model);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    //ModelState.AddModelError("", "Failed to create award");
                    //return PartialView("_Award", null);                    
                    return new JsonResult() { Data = new { success = false, message = "Failed to create award" } };
                }                
            }
            else
            {
                try
                {
                    var editModel = awardService.GetAwardById(model.Id);
                    editModel.Heading = model.Heading;
                    editModel.Content = model.Content;                    
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
                        editModel.PathToSecondPicture = secondPath;
                    }
                    
                    awardService.UpdateAward(editModel);                    
                    //return PartialView("_Award", editModel);
                    //return RedirectToAction("Index");
                    return new JsonResult() { Data = new { success = true } };
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    //ModelState.AddModelError("", "Failed to update award");
                    //return PartialView("_Award", model);
                    return new JsonResult() { Data = new { success = false, message = "Failed to update award" } };
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
            return Path.Combine("Content/Award/", fileName);
        }

        private AwardViewModel ToAwardViewModel(Award award)
        {
            return new AwardViewModel() 
            {
                Award = award,
                UserName = UserHelper.GetUserName(UserManager, award.UserId)
            };
        }
    }
}
