using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleMusicStore.Models
{
    public class LabelUser
    {
        [Required]
        public int LabelId { get; set; }
        public Label Label { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public SimpleUser User { get; set; }

        public DateTime DateFollowed => DateTime.UtcNow;
    }
}
