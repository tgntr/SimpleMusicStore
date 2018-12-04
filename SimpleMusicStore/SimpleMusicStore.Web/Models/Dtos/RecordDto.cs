using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Models.Dtos
{
    public class RecordDto
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }
    }
}
