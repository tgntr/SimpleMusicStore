using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public SimpleUser User { get; set; }

        public string Content { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
    }
}
