using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleMusicStore.Models
{
    public class Record
    {
        public int Id { get; set; }

        [Required]
        public int DiscogsId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public string Genre { get; set; }

        public DateTime ReleaseDate { get; set; }
        
        [NotMapped]
        public List<String> VideoUrls { get; set; }

        public List<Track> Tracks { get; set; } = new List<Track>();

        [Required]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        [Required]
        public int LabelId { get; set; }
        public Label Label { get; set; }
        
        [Required]
        public decimal Price { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<RecordUser> WantedBy { get; set; } = new List<RecordUser>();
    }
}
