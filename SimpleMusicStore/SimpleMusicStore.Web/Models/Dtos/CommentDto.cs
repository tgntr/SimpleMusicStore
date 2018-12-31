using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Models.Dtos
{
    public class CommentDto
    {
        public string User { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }

        public DateTime DatePosted { get; set; }

        public string DateFormat => DatePosted.ToString("MMMM dd, yyyy");

        public int Id { get; set; }

        public bool IsCreator { get; set; } = false;
    }
}
