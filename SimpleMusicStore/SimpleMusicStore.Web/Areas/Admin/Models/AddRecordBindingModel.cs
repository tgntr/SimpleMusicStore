using SimpleMusicStore.Web.Areas.Admin.Utilities;
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
        [DiscogsUrl(ErrorMessage = "Must be a url link to a Discogs release page")]
        [Display(Name = "Import from Discogs")]
        public string DiscogsUrl { get; set; }
    }
}
