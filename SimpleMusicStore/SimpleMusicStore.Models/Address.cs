using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public SimpleUser User { get; set; }
    }
}
