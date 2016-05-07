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

namespace Isdg.Controllers
{    
    public class NewsController : Controller
    {
        private readonly INewsService newsService;
        private ApplicationUserManager userManager;        
        
        [Inject]
        public NewsController(INewsService newsService) 
        {
            var context = new ApplicationDbContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));            
            this.newsService = newsService;            
        }
                
        public ActionResult Index()
        {            
            var news = newsService.GetAllNews();
            return View(ToNewsListViewModel(news));
        }
        
        [HttpPost]
        public ActionResult CreateEditNews(News model)
        {
            if (model.Id == 0)
            {                
                var currentDate = System.DateTime.Now;
                model.ModifiedDate = currentDate;
                model.AddedDate = currentDate;
                model.IP = Request.UserHostAddress;
                if (!User.IsInRole(UserRole.Admin.ToString()))
                    model.IsPublished = false;
                try
                {
                    model.UserId = User.Identity.GetUserId();                
                    newsService.InsertNews(model);
                    return PartialView("_News", ToNewsViewModel(model));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create news");
                    return PartialView("_News", null);
                }                
            }
            else
            {
                try
                {
                    var editModel = newsService.GetNewsById(model.Id);                
                    editModel.Content = model.Content;
                    editModel.IsPublished = model.IsPublished;
                    editModel.ModifiedDate = System.DateTime.Now;
                    editModel.IP = Request.UserHostAddress;                
                    newsService.UpdateNews(editModel);
                    return PartialView("_News", ToNewsViewModel(editModel));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to update news");
                    return PartialView("_News", model);
                }                
            }                     
        }        
        
        [HttpPost]
        public ActionResult ConfirmDeleteNews(int newsId)
        {
            try
            {
                var model = newsService.GetNewsById(newsId);            
                newsService.DeleteNews(model);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to delete news");
                return PartialView("_News", null);
            }            
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            News model = new News();
            if (id.HasValue)
            {
                model = newsService.GetNewsById(id.Value);
            }
            return View(model);
        }
                
        [HttpPost]
        [System.Web.Mvc.Authorize(Roles = "Admin")]
        public ActionResult EditNews(News model)
        {
            try
            {
                var editModel = newsService.GetNewsById(model.Id);
                editModel.Content = model.Content;
                editModel.IsPublished = model.IsPublished;
                editModel.ModifiedDate = System.DateTime.Now;
                editModel.IP = Request.UserHostAddress;                
                newsService.UpdateNews(editModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to edit news");
                return View("Edit", model);
            }            
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            News model = newsService.GetNewsById(id);
            return View(model);
        }

        private NewsViewModel ToNewsViewModel(News news)
        {
            var model = new NewsViewModel() { News = news, Show = news.IsPublished };            
            var user = userManager.Users.FirstOrDefault(x => x.Id == news.UserId);
            model.UserName = user == null ? "" : user.UserName;
            if (User.IsInRole(UserRole.Admin.ToString()))
            {
                model.CanDeleteNews = true;
                model.CanEditNews = true;
                model.CanSeeDetails = true;
                model.Show = true;                
            }
            else if (User.IsInRole(UserRole.Trusted.ToString()))
            {                
                if (User.Identity.GetUserId().Equals(news.UserId))
                {
                    model.CanEditNews = true;
                    model.Show = true;
                }                
            }            
            return model;
        }

        private NewsListViewModel ToNewsListViewModel(IEnumerable<News> news)
        {
            var model = new NewsListViewModel();
            model.CanCreateNews = User.IsInRole(UserRole.Admin.ToString()) || User.IsInRole(UserRole.Trusted.ToString()) || User.IsInRole(UserRole.Untrusted.ToString());
            model.NewsList = news.Select(ToNewsViewModel).ToList();
            return model;
        }
    }
}
