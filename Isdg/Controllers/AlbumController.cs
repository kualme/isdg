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
        private ITextService textService;

        public AlbumController(IAlbumService albumService, ITextService textService, ILog log, IEmailSender emailSender) : base(log, emailSender)
        {
            this.albumService = albumService;
            this.textService = textService;
        }
                
        public ActionResult Index()
        {
            var model = new AlbumsListViewModel();
            model.CanCreateAlbum = UserHelper.IsAdmin();
            var canDeleteAlbum = UserHelper.IsAdmin();
            var canEditAlbum = UserHelper.IsAdmin();
            var albums = albumService.GetAllAlbums();
            model.Albums = albums.Select(x => ToAlbumViewModel(x)).ToList();

            var key = "Albums";
            var text = textService.GetTextByKey(key);
            model.Text = text == null ? null : new TextViewModel() { Key = key, Content = text.Value, UserName = UserHelper.GetUserName(UserManager, text.UserId) };
            return View("Index", model);
        }

        public ActionResult Details(int? albumId)
        {
            if (albumId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = albumService.GetAlbumById(albumId.Value);
            if (album == null)
            {
                return HttpNotFound();
            }
            var model = new AlbumViewModel() { 
                Album = album,              
                Images = album.Images.Select(x => ToImageViewModel(x)).ToList()
            };
            return View(model);
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
                    return PartialView("_Album", ToAlbumViewModel(album));
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
                    return PartialView("_Album", ToAlbumViewModel(editModel));
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    ModelState.AddModelError("", "Failed to update album");
                    return PartialView("_Album", ToAlbumViewModel(album));
                }
            }            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditImage(Image image)
        {
            try
            {
                var editModel = albumService.GetImageById(image.Id);
                editModel.Caption = image.Caption;
                editModel.ModifiedDate = System.DateTime.Now;
                editModel.IP = Request.UserHostAddress;
                editModel.IsPublished = image.IsPublished;
                albumService.UpdateImage(editModel);
                return PartialView("_Image", ToImageViewModel(editModel));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", "Failed to update news");
                return PartialView("_Image", ToImageViewModel(image));
            }            
        }
                        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDeleteAlbum(int albumId)
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
                ModelState.AddModelError("", "Failed to delete album");
                return PartialView("_Album", null);
            }            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDeleteImage(int imageId)
        {
            try
            {
                var image = albumService.GetImageById(imageId);
                albumService.DeleteImage(image);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", "Failed to delete image");
                return PartialView("_Image", null);
            }
        }

        public ActionResult UploadImages(int albumId)
        {
            var album = albumService.GetAlbumById(albumId);
            var userId = User.Identity.GetUserId();
            var isPublished = UserHelper.IsAdmin();
            if (Request.Files.Count == 0)
                return new JsonResult() { Data = new { message = "There is no images to add" } };

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
            else message += ". Refresh the page to see the changes.";
            return new JsonResult() { Data = new { message } };
        }

        [HttpPost]
        public ActionResult CreateText(TextViewModel model)
        {
            var currentDate = DateTime.Now;
            var text = new Text()
            {
                Key = model.Key,
                Value = model.Content,
                UserId = User.Identity.GetUserId(),
                AddedDate = currentDate,
                ModifiedDate = currentDate,
                IP = Request.UserHostAddress
            };
            textService.InsertText(text);
            return Index();
        }

        public ActionResult EditText()
        {
            var key = "Albums";
            var text = textService.GetTextByKey(key);
            if (text != null)
            {
                var model = new TextViewModel()
                {
                    Key = key,
                    Content = text.Value
                };
                return View("EditText", model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditText(TextViewModel model)
        {
            var text = textService.GetTextByKey(model.Key);
            if (text != null)
            {
                text.Value = model.Content;
                textService.UpdateText(text);                                
            }
            return RedirectToAction("Index");
        }

        private AlbumViewModel ToAlbumViewModel(Album album)
        {
            var model = new AlbumViewModel() { 
                Album = album,
                CanDeleteAlbum = UserHelper.IsAdmin(),
                CanEditAlbumName = UserHelper.IsAdmin(),
                CanEditAlbumContent = UserHelper.IsAdminOrTrusted()                
            };
            if (album.Images != null)
                model.Images = album.Images.Select(x => ToImageViewModel(x)).ToList();
            return model;
        }

        private ImageViewModel ToImageViewModel(Image image)
        {
            var model = new ImageViewModel() { Image = image };
            if (UserHelper.IsAdmin())
            {
                model.CanEditImage = true;
                model.CanDeleteImage = true;
                model.Show = true;
            }
            else if (UserHelper.IsTrusted()) 
            {
                if (User.Identity.GetUserId().Equals(image.UserId))
                {
                    model.CanDeleteImage = true;
                    model.CanEditImage = true;
                    model.Show = true;
                }
            }
            return model;
        }
    }
}
