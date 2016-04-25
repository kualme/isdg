using System;
using System.Collections.Generic;
using System.Linq;
using Isdg.Data;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// News service
    /// </summary>
    public class NewsService : INewsService
    {
        private readonly IRepository<News> _newsRepository;
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="_newsRepository">News repository</param>        
        public NewsService(IRepository<News> newsRepository)
        {
            this._newsRepository = newsRepository;
        }
        
        /// <summary>
        /// Delete news
        /// </summary>
        /// <param name="news">News</param>
        public virtual void DeleteNews(News news)
        {
            if (news == null)
                throw new ArgumentNullException("news");                        
            _newsRepository.Delete(news);            
        }
        
        /// <summary>
        /// Get all news
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>News</returns>
        public virtual IPagedList<News> GetAllNews(int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _newsRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.IsPublished);
                        
            query = query.OrderByDescending(c => c.AddedDate);
            
            if (!showHidden)
            {
                // ��� ���� ��� ���� ��������
                // ���� ������������ ����� ��� trusted member, �� ���������� ����������?
                //ACL (access control list)
                //var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                //    .Where(cr => cr.Active).Select(cr => cr.Id).ToList();                
            }
                        
            //paging
            return new PagedList<News>(query.ToList(), pageIndex, pageSize);
        }
                
        /// <summary>
        /// Get news
        /// </summary>
        /// <param name="newsId">News identifier</param>
        /// <returns>News</returns>
        public virtual News GetNewsById(int newsId)
        {
            if (newsId == 0)
                return null;
            return _newsRepository.GetById(newsId);            
        }

        /// <summary>
        /// Insert news
        /// </summary>
        /// <param name="news">News</param>
        public virtual void InsertNews(News news)
        {
            if (news == null)
                throw new ArgumentNullException("news");
            _newsRepository.Insert(news);
        }

        /// <summary>
        /// Update news
        /// </summary>
        /// <param name="news">News</param>
        public virtual void UpdateNews(News news)
        {
            if (news == null)
                throw new ArgumentNullException("news");            
            _newsRepository.Update(news);
        }
    }
}
