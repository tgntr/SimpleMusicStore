using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string UserId { get; set; }
        public SimpleUser User { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
