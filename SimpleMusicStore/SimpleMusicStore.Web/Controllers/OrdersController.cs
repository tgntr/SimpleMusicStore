using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Web.Models.ViewModels;
using SimpleMusicStore.Web.Services;

namespace SimpleMusicStore.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;
        private readonly RecordService _recordService;
        private readonly AddressService _addressService;
        private readonly IMapper _mapper;
        private readonly string _referrerUrl;




        public OrdersController(SimpleDbContext context, IMapper mapper)
        {
            _orderService = new OrderService(context, HttpContext.Session, mapper);
            _recordService = new RecordService(context);
            _addressService = new AddressService(context);
            _mapper = mapper;
            _referrerUrl = Request.Headers["Referer"].ToString();
        }



        public IActionResult Details(int orderId)
        {
            var order = _orderService.GetOrder(orderId);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (order == null || order.UserId != userId)
            {
                return Redirect("/profile/orders");
            }

            var model = _mapper.Map<CartOrderViewModel>(order);

            return View(model);
        }



        public IActionResult Cart()
        {
            var model = GetCart();

            return View(model);
        }


        public IActionResult Preview()
        {
            var cart = GetCart();

            if (cart.TotalPrice <= 0)
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty!");

                return Redirect("/orders/cart");
            }

            return View(cart);
        }


        [HttpPost]
        public async Task<IActionResult> Preview(CartOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please select delivery address!");

                return View(model);
            }
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderId = await _orderService.Order(model, userId);

            return Redirect($"/orders/details/{orderId}");
        }
        


        public IActionResult AddToCart(int recordId)
        {
            _orderService.AddToCart(recordId);

            return Redirect(_referrerUrl);
        }



        public IActionResult RemoveFromCart(int recordId)
        {
            _orderService.RemoveFromCart(recordId);

            return Redirect(_referrerUrl);
        }


        public IActionResult DecreaseQuantity(int recordId)
        {
            _orderService.DecreaseQuantity(recordId);

            return Redirect(_referrerUrl);
        }



        public IActionResult EmptyCart(int recordId)
        {
            _orderService.EmptyCart();

            return Redirect(_referrerUrl);
        }




        private CartOrderViewModel GetCart()
        {
            var items = _orderService.GetCart()
                .Select(c =>
                {
                    var cartRecordViewModel = _mapper.Map<CartRecordViewModel>(_recordService.GetRecord(c.RecordId));
                    cartRecordViewModel.Quantity = c.Quantity;
                    return cartRecordViewModel;
                })
                .ToList();

            return new CartOrderViewModel { Items = items };
        }
    }
}