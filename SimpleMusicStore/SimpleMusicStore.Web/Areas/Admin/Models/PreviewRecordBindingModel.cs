using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Areas.Admin.Models
{
    public class PreviewRecordBindingModel
    {
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "The price must be non-negative")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
