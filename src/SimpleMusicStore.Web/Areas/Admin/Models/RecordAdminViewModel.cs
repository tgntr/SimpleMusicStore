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

        public int Year { get; set; }

        public string Genre { get; set; }
        
        [Required]
        [Range(1, 100.00, ErrorMessage = "Must be between 1$ and 100$")]
        [Display(Name = "Set Price")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Format { get; set; }
    }
}
