using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Isdg.Core.Data;
using Isdg.Models;
using Isdg.Services.Information;
using System.IO;
using Microsoft.AspNet.Identity;
using log4net;
using Isdg.Services.Messages;
using Isdg.Lib;

namespace Isdg.Controllers
{
    public class AlbumController : BaseController
    {
        private readonly IAlbumService albumService;        

        public AlbumController(IAlbumService albumService, ILog log, IEmailSender emailSender) : base(log, emailSender)
        {
            this.albumService = albumService;            
        }
                
        public ActionResult Index()
        {
            var albums = albumService.GetAllAlbums();
            return View(albums);
        }

        // GET: /Album/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = albumService.GetAlbumById(id.Value);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEditAlbum([Bind(Include="Id,Name,AddedDate,ModifiedDate,IP")] Album album)
        {
            if (album.Id == 0)
            {
                var currentDate = System.DateTime.Now;
                album.ModifiedDate = currentDate;
                album.AddedDate = currentDate;
                album.IP = Request.UserHostAddress;
                try
                {
                    albumService.InsertAlbum(album);
                    return PartialView("_Album", album);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    ModelState.AddModelError("", "Failed to create album");
                    return PartialView("_Album", null);
                }
            }
            else
            {
                try
                {
                    var editModel = albumService.GetAlbumById(album.Id);
                    editModel.Name = album.Name;                    
                    editModel.ModifiedDate = System.DateTime.Now;
                    editModel.IP = Request.UserHostAddress;
                    albumService.UpdateAlbum(editModel);
                    return PartialView("_Album", editModel);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    ModelState.AddModelError("", "Failed to update news");
                    return PartialView("_Album", album);
                }
            }            
        }

        // GET: /Album/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = albumService.GetAlbumById(id.Value);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: /Album/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,AddedDate,ModifiedDate,IP")] Album album)
        {
            if (ModelState.IsValid)
            {
                albumService.UpdateAlbum(album);                
                return RedirectToAction("Index");
            }
            return View(album);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmeDeleteAlbum(int albumId)
        {
            try
            {
                var album = albumService.GetAlbumById(albumId);
                albumService.DeleteAlbum(album);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", "Failed to delete news");
                return PartialView("_Album", null);
            }            
        }

        public ActionResult UploadImages(int albumId)
        {
            var album = albumService.GetAlbumById(albumId);
            var userId = User.Identity.GetUserId();
            var isPublished = UserHelper.IsAdminOrTrusted();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var extension = Path.GetExtension(file.FileName);
                var fileGuid = Guid.NewGuid();
                var fileName = fileGuid + extension;
                var path = Path.Combine(Server.MapPath("~/Content/Albums/"), fileName);
                file.SaveAs(path);                
                var currentDate = System.DateTime.Now;
                var image = new Image()
                {
                    Album = album,
                    IsPublished = isPublished,
                    AddedDate = currentDate,
                    ModifiedDate = System.DateTime.Now,
                    IP = Request.UserHostAddress,
                    Caption = "",
                    Path = Path.Combine("Content/Albums/", fileName),
                    PathToPreview = "",
                    UserId = userId                    
                };
                albumService.InsertImage(image);
            }
            var message = Request.Files.Count == 1 ? "Image has been added successfully" : "Images have been added successfully";
            if (!isPublished)
                message += ". They will be published after validation.";
            return new JsonResult() { Data = new { message } };
        }
    }
}
