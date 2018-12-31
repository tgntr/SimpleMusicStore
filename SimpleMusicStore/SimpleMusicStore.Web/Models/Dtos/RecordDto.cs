using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Models.Dtos
{
    public class RecordDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public DateTime DateAdded { get; set; }

        public LabelDto Label { get; set; }

        public ArtistDto Artist { get; set; }

        public bool IsActive { get; set; }
    }
}
