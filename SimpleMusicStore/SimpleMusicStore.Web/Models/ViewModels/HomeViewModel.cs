using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<RecordViewModel> MostPopularRecords { get; set; }

        public List<ArtistViewModel> MostPopularArtists { get; set; }

        public List<LabelViewModel> MostPopularLabels { get; set; } 

        public List<RecordViewModel> RecommendedRecords { get; set; }
    }
}
