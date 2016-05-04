using System;
using System.Collections.Generic;
using System.Linq;
using Isdg.Data;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Image service
    /// </summary>
    public class ImageService : IImageService
    {
        private readonly IRepository<Image> _imageRepository;
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="_imageRepository">Image repository</param>        
        public ImageService(IRepository<Image> imageRepository)
        {
            this._imageRepository = imageRepository;
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
