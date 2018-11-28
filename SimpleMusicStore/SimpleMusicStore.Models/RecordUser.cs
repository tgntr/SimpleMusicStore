using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Models
{
    public class RecordUser
    {
        public int RecordId { get; set; }
        public Record Record { get; set; }

        public string UserId { get; set; }
        public SimpleUser User { get; set; }
    }
}
