using System.Collections.Generic;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// News service interface
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// Delete news
        /// </summary>
        /// <param name="News">News</param>
        void DeleteNews(News news);

        /// <summary>
        /// Get all news
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>News</returns>
        IPagedList<News> GetAllNews(int pageIndex = 0, int pageSize = int.MaxValue);
                
        /// <summary>
        /// Get news
        /// </summary>
        /// <param name="newsId">News identifier</param>
        /// <returns>News</returns>
        News GetNewsById(int newsId);

        /// <summary>
        /// Insert news
        /// </summary>
        /// <param name="news">News</param>
        void InsertNews(News news);

        /// <summary>
        /// Update news
        /// </summary>
        /// <param name="news">News</param>
        void UpdateNews(News news);                
    }
}
