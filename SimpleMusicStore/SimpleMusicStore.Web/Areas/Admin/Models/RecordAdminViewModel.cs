using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Areas.Admin.Models
{
    public class RecordAdminViewModel
    {
        public string Title { get; set; }

        public string Artist { get; set; }

        public string Label { get; set; }

        public decimal? Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
