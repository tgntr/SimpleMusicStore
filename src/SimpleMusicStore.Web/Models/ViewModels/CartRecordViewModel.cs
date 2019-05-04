using SimpleMusicStore.Web.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Models.ViewModels
{
    public class CartRecordViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }
        
        
        public ArtistDto Artist { get; set; }

        public LabelDto Label { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }
}
