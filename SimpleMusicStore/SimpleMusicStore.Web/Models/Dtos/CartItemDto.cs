using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Models.Dtos
{
    public class CartItemDto
    {
        public int RecordId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
