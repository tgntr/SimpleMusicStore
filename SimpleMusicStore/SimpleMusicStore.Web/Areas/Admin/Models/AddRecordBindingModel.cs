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
        [UrlAttribute]
        [Display(Name = "Discogs url link")]
        public string DiscogsUrl { get; set; }
    }
}
