using SimpleMusicStore.Web.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Models.ViewModels
{
    public class RecordViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public int Year { get; set; }

        public List<VideoDto> Videos { get; set; }

        public List<TrackDto> Tracks { get; set; }

        public ArtistDto Artist { get; set; }

        public LabelDto Label { get; set; }

        public List<CommentDto> Comments { get; set; }

        [Required]
        public string Comment { get; set; }
        
    }
}
