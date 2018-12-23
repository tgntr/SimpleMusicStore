using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Models.Dtos;
using SimpleMusicStore.Web.Models.ViewModels;
using SimpleMusicStore.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Services
{
    internal class OrderService : Service
    {
        private const string _cart = "cart";

        private readonly ISession _session;
        private readonly RecordService _recordService;
        private readonly IMapper _mapper;

        public OrderService(SimpleDbContext context, ISession session, IMapper mapper)
            : base(context)
        {
            _session = session;
            _recordService = new RecordService(context);
            _mapper = mapper;
        }



        internal void AddToCart(int recordId)
        {
            var itemToAdd = new CartItemDto { RecordId = recordId };
            var cart = GetCart();
            if (cart == null)
            {
                cart = new List<CartItemDto>();
                
            }

            var item = cart.FirstOrDefault(i => i.RecordId == recordId);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Add(itemToAdd);
            }

            _session.SetString(_cart, JsonConvert.SerializeObject(cart));
        }
        
        internal void DecreaseQuantity(int recordId)
        {
            var cart = GetCart();
            if (cart == null)
            {
                return;
            }

            var item = cart.FirstOrDefault(i => i.RecordId == recordId);
            if (item == null)
            {
                return;
            }
            
            if (item.Quantity == 1)
            {
                cart.Remove(item);
            }
            else
            {
                item.Quantity--;
            }

            _session.SetString(_cart, JsonConvert.SerializeObject(cart));
        }


        internal void RemoveFromCart(int recordId)
        {
            var cart = GetCart();
            if (cart == null)
            {
                return;
            }

            var item = cart.FirstOrDefault(i => i.RecordId == recordId);
            if (item == null)
            {
                return;
            }

            cart.Remove(item);

            _session.SetString(_cart, JsonConvert.SerializeObject(cart));
        }



        internal async Task<int> Order(CartOrderViewModel model, string userId)
        {
            var cart = GetCart();

            if (cart == null)
            {
                return -1;
            }

            var order = new Order { DeliveryAddressId = model.DeliveryAddressId, UserId = userId, TotalPrice = model.TotalPrice};

            order.Items = cart.Select(_mapper.Map<RecordOrder>).ToList();
            

            

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            EmptyCart();

            return order.Id;
        }



        internal List<CartItemDto> GetCart()
        {
            var value = _session.GetString(_cart);
            return value == null ? null : JsonConvert.DeserializeObject<List<CartItemDto>>(value);
        }

        internal async Task<Order> GetOrder(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Record)
                        .ThenInclude(r=>r.Artist)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Record)
                        .ThenInclude(r => r.Label)
                .Include(o=>o.DeliveryAddress)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        internal void EmptyCart()
        {
            _session.SetString(_cart, "");
        }
    }
}
