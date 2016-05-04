using System.Collections.Generic;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Image service interface
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Delete image
        /// </summary>
        /// <param name="News">Image</param>
        void DeleteImage(Image image);
                
        /// <summary>
        /// Get all image
        /// </summary>        
        /// <param name="albumId">Album identifier</param>
        /// <returns>Images</returns>
        List<Image> GetImagesFromAlbum(int albumId);
                
        /// <summary>
        /// Get image
        /// </summary>
        /// <param name="imageId">Image identifier</param>
        /// <returns>Image</returns>
        Image GetImageById(int imageId);

        /// <summary>
        /// Insert Image
        /// </summary>
        /// <param name="image">Image</param>
        void InsertImage(Image image);

        /// <summary>
        /// Update image
        /// </summary>
        /// <param name="image">image</param>
        void UpdateImage(Image image);
    }
}
