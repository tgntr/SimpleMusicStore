using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int DeliveryAddressId { get; set; }
        public Address DeliveryAddress { get; set; }

        [Required]
        public string UserId { get; set; }
        public SimpleUser User { get; set; }

        public List<RecordOrder> Items { get; set; }

        //public Cart Items {get;set;}

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        public decimal TotalPrice { get; set; }


    }
}
