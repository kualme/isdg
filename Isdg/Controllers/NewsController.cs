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

namespace Isdg.Controllers
{    
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        
        [Inject]
        public NewsController(INewsService newsService) 
        {
            this._newsService = newsService;
        }
        
        public ActionResult Index()
        {
            var news = _newsService.GetAllNews(0, int.MaxValue, true);
            return View(news);
        }
        
        public ActionResult Edit(int? id)
        {
            News model = new News();
            if (id.HasValue)
            {
                model = _newsService.GetNewsById(id.Value);
            }
            return View(model);
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
                _newsService.InsertNews(model);
                return PartialView("_News", model);
            }
            else
            {
                var editModel = _newsService.GetNewsById(model.Id);
                editModel.Content = model.Content;
                editModel.IsPublished = model.IsPublished;
                editModel.ModifiedDate = System.DateTime.Now;
                editModel.IP = Request.UserHostAddress;
                _newsService.UpdateNews(editModel);
                return PartialView("_News", editModel);
            }                     
        }

        [HttpPost]
        public ActionResult EditNews(News model)
        {
            if (model.Id == 0)
            {                
                return RedirectToAction("Index");
            }
            else
            {
                var editModel = _newsService.GetNewsById(model.Id);
                editModel.Content = model.Content;
                editModel.IsPublished = model.IsPublished;
                editModel.ModifiedDate = System.DateTime.Now;
                editModel.IP = Request.UserHostAddress;
                _newsService.UpdateNews(editModel);
                return RedirectToAction("Index");
            }
        }
        
        [HttpPost]
        public ActionResult ConfirmDeleteNews(int newsId)
        {
            var model = _newsService.GetNewsById(newsId);
            _newsService.DeleteNews(model);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Details(int id)
        {
            News model = _newsService.GetNewsById(id);
            return View(model);
        }
    }
}
