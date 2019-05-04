using SimpleMusicStore.Data.Models;
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
        [Required(ErrorMessage = "Please choose delivery address")]
        public int DeliveryAddressId { get; set; }

        public AddressDto DeliveryAddress { get; set; }
        
        public List<AddressDto> Addresses { get; set; }

        public List<CartRecordViewModel> Items { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public string OrderDateFormat => OrderDate.ToString("MMMM dd, yyyy");


        public string SessionId { get; set; }

        public decimal TotalPrice()
        {
            return Items.Sum(i => i.Quantity * i.Price);
        }
    }
}
