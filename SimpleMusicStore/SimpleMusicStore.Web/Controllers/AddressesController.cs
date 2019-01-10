using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Models.Dtos;
using SimpleMusicStore.Web.Services;

namespace SimpleMusicStore.Web.Controllers
{
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly AddressService _addressService;
        private readonly IMapper _mapper;




        public AddressesController(SimpleDbContext context, IMapper mapper)
        {
            _addressService = new AddressService(context);
            _mapper = mapper;
        }



        public IActionResult Add()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Add(AddressDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var address = _mapper.Map<Address>(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            address.UserId = userId;

            await _addressService.AddAsync(address);

            return Redirect("/profile/addresses");
        }




        public async Task<IActionResult> Edit(int addressId)
        {
            var address = await _addressService.GetAsync(addressId);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (address == null || address.UserId != userId)
            {
                return Redirect("/profile/addresses");
            }

            var model = _mapper.Map<AddressDto>(address);

            return View(model);

        }



        [HttpPost]
        public async Task<IActionResult> Edit(AddressDto model, int addressId)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _addressService.EditAsync(model, addressId);

            return Redirect("/profile/addresses");
        }



        public async Task<IActionResult> Remove(int addressId)
        {
            var address = await _addressService.GetAsync(addressId);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (address == null || address.UserId != userId)
            {
                return Redirect("/profile/addresses");
            }

            await _addressService.RemoveAsync(addressId);

            return Redirect("/profile/addresses");
        }
    }
}