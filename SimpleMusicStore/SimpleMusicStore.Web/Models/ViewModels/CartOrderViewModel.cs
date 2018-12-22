using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Models.ViewModels
{
    public class CartOrderViewModel
    {
        [Required]
        public int DeliveryAddressId { get; set; }

        public AddressDto DeliveryAddress { get; set; }

        public List<CartRecordViewModel> Items { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

    }
}
