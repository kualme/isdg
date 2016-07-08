using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Isdg.Core.Data;

namespace Isdg.Models
{
    public class AlbumsListViewModel
    {
        public AlbumsListViewModel()
        {
            Albums = new List<AlbumViewModel>();
            Text = new TextViewModel();
        }

        public bool CanCreateAlbum { get; set; }
        public List<AlbumViewModel> Albums { get; set; }
        public TextViewModel Text { get; set; }
    }

    public class AlbumViewModel
    {
        public AlbumViewModel()
        {
            Images = new List<ImageViewModel>();
        }

        public bool CanDeleteAlbum { get; set; }
        public bool CanEditAlbumContent { get; set; }
        public bool CanEditAlbumName { get; set; }
        public Album Album { get; set; }
        public List<ImageViewModel> Images { get; set; }
        public bool Collapsed { get; set; }
    }
}