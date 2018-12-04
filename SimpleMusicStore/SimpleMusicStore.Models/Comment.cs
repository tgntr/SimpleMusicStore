using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public SimpleUser User { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.UtcNow;

        public int? RecordId { get; set; }
        public Record Record { get; set; }

        public int? ArtistId { get; set; }
        public Artist Artist { get; set; }

        public int? LabelId { get; set; }
        public Label Label { get; set; }
    }
}
