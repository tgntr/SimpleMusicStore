using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleMusicStore.Models
{
    public class ArtistUser
    {
        [Required]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public SimpleUser User { get; set; }
    }
}
