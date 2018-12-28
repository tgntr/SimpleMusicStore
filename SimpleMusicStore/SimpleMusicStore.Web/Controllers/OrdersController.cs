using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Web.Models.Dtos;
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




        public OrdersController(SimpleDbContext context, IMapper mapper)
        {
            _orderService = new OrderService(context, mapper);
            _recordService = new RecordService(context);
            _addressService = new AddressService(context);
            _mapper = mapper;
        }



        public async Task<IActionResult> Details(int orderId)
        {
            var order = await _orderService.GetOrder(orderId);

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


        public async Task<IActionResult> Preview(string sessionId)
        {
            if (sessionId != HttpContext.Session.Id)
            {
                return RedirectToAction("Cart");
            }

            var model = GetCart();
            
            if (model.Items.Count() == 0)
            {
                return RedirectToAction("Cart");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.Addresses = (await _addressService.AllUserAddresses(userId)).Select(_mapper.Map<AddressDto>).ToList();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Preview(string sessionId, CartOrderViewModel model)
        {
            if (sessionId != HttpContext.Session.Id)
            {
                return RedirectToAction("Cart");
            }

            var cart = GetCart();

            if (cart.Items.Count() == 0)
            {
                return RedirectToAction("Cart");
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please select delivery address!");

                return View(cart);
            }


            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderId = await _orderService.Order(model, userId, HttpContext.Session, cart.TotalPrice());

            return Redirect($"/orders/details?orderId={orderId}");
        }
        


        public IActionResult AddToCart(int recordId)
        {
            _orderService.AddToCart(recordId, HttpContext.Session);

            return RedirectToAction("Cart");
        }



        public IActionResult RemoveFromCart(int recordId)
        {
            _orderService.RemoveFromCart(recordId, HttpContext.Session);

            return RedirectToAction("Cart");
        }


        public IActionResult DecreaseQuantity(int recordId)
        {
            _orderService.DecreaseQuantity(recordId, HttpContext.Session);

            return RedirectToAction("Cart");
        }



        public IActionResult EmptyCart(int recordId)
        {
            _orderService.EmptyCart(HttpContext.Session);

            return RedirectToAction("Cart");
        }




        private CartOrderViewModel GetCart()
        {
            List<CartRecordViewModel> items = new List<CartRecordViewModel>();
            List<CartItemDto> cart = _orderService.GetCart(HttpContext.Session);
            if (cart != null)
            {
                items = _orderService.GetCart(HttpContext.Session)
                    .Select(c =>
                    {
                        var record = _recordService.GetRecord(c.RecordId);
                        var cartRecordViewModel = _mapper.Map<CartRecordViewModel>(record);
                        cartRecordViewModel.Quantity = c.Quantity;
                        return cartRecordViewModel;
                    })
                    .ToList();
            }

            return new CartOrderViewModel { Items =  items, SessionId = HttpContext.Session.Id };
        }
    }
}