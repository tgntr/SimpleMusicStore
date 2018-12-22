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

        [Url]
        public string ImageUrl { get; set; } = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/12in-Vinyl-LP-Record-Angle.jpg/330px-12in-Vinyl-LP-Record-Angle.jpg";

        public string Description { get; set; }

        public string Genre { get; set; }

        [Required]
        public int Year { get; set; }

        public List<Video> Videos { get; set; } = new List<Video>();

        public List<Track> Tracks { get; set; } = new List<Track>();

        [Required]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        [Required]
        public int LabelId { get; set; }
        public Label Label { get; set; }
        

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<RecordUser> WantedBy { get; set; } = new List<RecordUser>();

        public List<RecordOrder> Orders { get; set; } = new List<RecordOrder>();

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        [Required]
        [Range(1, 100)]
        public decimal Price { get; set; }

        //quantity
    }
}
