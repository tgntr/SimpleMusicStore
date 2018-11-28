using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Areas.Admin.Models.RecordDtos
{
    public class RecordDto
    {
        public RecordVideoDto[] Videos { get; set; }

        public RecordLabelDto[] Labels { get; set; }

        public RecordArtistDto[] Artists { get; set; }

        public RecordImageDto[] Images { get; set; }

        public string[] Genres { get; set; }

        public string Title { get; set; }

        public RecordFormatDto[] Formats { get; set; }

        public string Released { get; set; }

        public RecordTrackDto[] Tracklist { get; set; }


    }
}
