using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SimpleMusicStore.Models
{
    public class SimpleUser : IdentityUser
    {
        public int? AddressId { get; set; }
        public Address Address { get; set; }
    }
}
