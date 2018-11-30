using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Areas.Admin.Models
{
    public class AddRecordBindingModel
    {
        [Required]
        [Url(ErrorMessage = "Must be a valid url")]
        [RegularExpression(@"https:\/\/www\.discogs\.com\/([^\/]+\/)?((release)|(master))\/[0-9]+([^\/]+)?", ErrorMessage = "Must be a Discogs release url")]
        [Display(Name = "Import from Discogs")]
        public string DiscogsUrl { get; set; }
    }
}
