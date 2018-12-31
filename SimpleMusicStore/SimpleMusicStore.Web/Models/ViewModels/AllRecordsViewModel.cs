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

        public List<string> AllGenres { get; set; }
        
        public List<string> SelectedFormats { get; set; } = new List<string>();

        public List<string> AllFormats { get; set; }

        public string Sort { get; set; }
    }
}
