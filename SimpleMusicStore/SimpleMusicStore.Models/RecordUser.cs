using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Models
{
    public class RecordUser
    {
        [Required]
        public int RecordId { get; set; }
        public Record Record { get; set; }

        [Required]
        public string UserId { get; set; }
        public SimpleUser User { get; set; }
    }
}
