using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Models.ViewModels
{
    public class AllRecordsViewModel
    {
        public List<RecordViewModel> Records { get; set; }

        public List<string> SelectedGenres { get; set; } = new List<string>();

        public List<string> AllGenres => Records.Select(r => r.Genre).Distinct().ToList();

        public string Sort { get; set; }
    }
}
