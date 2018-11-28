using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Models
{
    public class Artist
    {
        public int Id { get; set; }

        [Required]
        public int DiscogsId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public List<Record> Records { get; set; } = new List<Record>();

        public List<ArtistUser> Followers { get; set; } = new List<ArtistUser>();

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
