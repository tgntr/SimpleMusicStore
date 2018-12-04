using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Areas.Admin.Models
{
    public class RecordAdminViewModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Artist")]
        public string Artist { get; set; }

        [Display(Name = "Label")]
        public string Label { get; set; }
        
        [Required]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "Must be a valid price")]
        [Range(1, 100, ErrorMessage = "Must be between 1$ and 100$")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        
        public string ImageUrl { get; set; }
    }
}
