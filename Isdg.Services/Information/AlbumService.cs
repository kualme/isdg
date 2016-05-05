using System;
using System.Collections.Generic;
using System.Linq;
using Isdg.Data;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Album service
    /// </summary>
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Image> _imageRepository;
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="_albumRepository">Album repository</param>        
        public AlbumService(IRepository<Album> albumRepository, IRepository<Image> _imageRepository)
        {
            this._albumRepository = albumRepository;
            this._imageRepository = _imageRepository;
        }
        
        /// <summary>
        /// Delete album
        /// </summary>
        /// <param name="album">Album</param>
        public virtual void DeleteAlbum(Album album)
        {
            if (album == null)
                throw new ArgumentNullException("album");                        
            _albumRepository.Delete(album);            
        }
        
        /// <summary>
        /// Get all album
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>        
        /// <returns>Album</returns>
        public virtual IPagedList<Album> GetAllAlbums(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _albumRepository.Table;
            query = query.OrderByDescending(c => c.AddedDate);            
                        
            //paging
            return new PagedList<Album>(query.ToList(), pageIndex, pageSize);
        }
                
        /// <summary>
        /// Get album
        /// </summary>
        /// <param name="albumId">Album identifier</param>
        /// <returns>Album</returns>
        public virtual Album GetAlbumById(int albumId)
        {
            if (albumId == 0)
                return null;
            return _albumRepository.GetById(albumId);            
        }

        /// <summary>
        /// Insert album
        /// </summary>
        /// <param name="album">Album</param>
        public virtual void InsertAlbum(Album album)
        {
            if (album == null)
                throw new ArgumentNullException("album");
            _albumRepository.Insert(album);
        }

        /// <summary>
        /// Update album
        /// </summary>
        /// <param name="album">Album</param>
        public virtual void UpdateAlbum(Album album)
        {
            if (album == null)
                throw new ArgumentNullException("album");            
            _albumRepository.Update(album);
        }

        /// <summary>
        /// Delete image
        /// </summary>
        /// <param name="image">Image</param>
        public virtual void DeleteImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException("image");
            _imageRepository.Delete(image);
        }

        /// <summary>
        /// Get all image
        /// </summary>                
        /// <param name="albumId">Album identifier</param>
        /// <returns>Image</returns>
        public virtual List<Image> GetImagesFromAlbum(int albumId)
        {
            var query = _imageRepository.Table;
            query = query.Where(i => i.Album.Id == albumId).OrderBy(c => c.AddedDate);

            //paging
            return new List<Image>(query.ToList());
        }

        /// <summary>
        /// Get image
        /// </summary>
        /// <param name="imageId">Image identifier</param>
        /// <returns>Image</returns>
        public virtual Image GetImageById(int imageId)
        {
            if (imageId == 0)
                return null;
            return _imageRepository.GetById(imageId);
        }

        /// <summary>
        /// Insert image
        /// </summary>
        /// <param name="image">Image</param>
        public virtual void InsertImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException("image");
            _imageRepository.Insert(image);
        }

        /// <summary>
        /// Update image
        /// </summary>
        /// <param name="image">Image</param>
        public virtual void UpdateImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException("image");
            _imageRepository.Update(image);
        }
    }
}
