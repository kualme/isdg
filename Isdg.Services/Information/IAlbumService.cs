using System.Collections.Generic;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Album service interface
    /// </summary>
    public interface IAlbumService
    {
        /// <summary>
        /// Delete album
        /// </summary>
        /// <param name="News">Album</param>
        void DeleteAlbum(Album album);

        /// <summary>
        /// Get all album
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Albums</returns>
        IPagedList<Album> GetAllAlbums(int pageIndex = 0, int pageSize = int.MaxValue);
                
        /// <summary>
        /// Get album
        /// </summary>
        /// <param name="albumId">Album identifier</param>
        /// <returns>Album</returns>
        Album GetAlbumById(int albumId);

        /// <summary>
        /// Insert Album
        /// </summary>
        /// <param name="album">Album</param>
        void InsertAlbum(Album album);

        /// <summary>
        /// Update album
        /// </summary>
        /// <param name="album">album</param>
        void UpdateAlbum(Album album);                
    }
}
