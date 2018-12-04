using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Areas.Admin.Models.DiscogsDtos.RecordDtos
{
    public class DiscogsRecordDto
    {
        public DiscogsRecordVideoDto[] Videos { get; set; }

        public DiscogsRecordLabelDto[] Labels { get; set; }

        public DiscogsRecordArtistDto[] Artists { get; set; }

        public DiscogsImageDto[] Images { get; set; }

        public int Id { get; set; }

        public string[] Genres { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public DiscogsRecordTrackDto[] Tracklist { get; set; }


    }
}
