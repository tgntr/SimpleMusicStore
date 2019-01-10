using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Models.BindingModels;
using SimpleMusicStore.Web.Models.Dtos;
using SimpleMusicStore.Web.Models.ViewModels;
using SimpleMusicStore.Web.Services;

namespace SimpleMusicStore.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<SimpleUser> _userManager;
        private readonly SignInManager<SimpleUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;


        public ProfileController(
           UserManager<SimpleUser> userManager,
           SignInManager<SimpleUser> signInManager,
           SimpleDbContext context,
           RoleManager<IdentityRole> roleManager,
           IMapper mapper
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }


        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _mapper.Map<SimpleUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);



            if (result.Succeeded)
            {

                await AssignToRole(user);


                await _signInManager.SignInAsync(user, isPersistent: false);
                return Redirect("/");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();

        }




        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }

      

        [HttpPost]
        public async Task<IActionResult> Login(LoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Redirect("/");
        }


        [Authorize]
        public async Task<IActionResult> Wantlist ()
        {
            var user = await _userManager.Users
                .Include(u => u.Wantlist)
                   .ThenInclude(w => w.Record)
                        .ThenInclude(r=>r.Artist)
                .Include(u => u.Wantlist)
                   .ThenInclude(w => w.Record)
                        .ThenInclude(r => r.Label)
                .SingleAsync(u => u.Id == GetUserId);

            var model = user.Wantlist.OrderByDescending(w=>w.DateFollowed).Select(_mapper.Map<RecordDto>).ToList();

            return View(model);
        }



        [Authorize]
        public async Task<IActionResult> FollowedArtists()
        {
            var user = await _userManager.Users
                .Include(u => u.FollowedArtists)
                   .ThenInclude(al => al.Artist)
                .SingleAsync(u => u.Id == GetUserId);

            var model = user.FollowedArtists.OrderByDescending(fa => fa.DateFollowed).Select(_mapper.Map<ArtistDto>).ToList();

            return View(model);
        }



        [Authorize]
        public async Task<IActionResult> FollowedLabels()
        {
            var user = await _userManager.Users
                .Include(u => u.FollowedLabels)
                    .ThenInclude(fl=>fl.Label)
                .SingleAsync(u => u.Id == GetUserId);

            var model = user.FollowedLabels.OrderByDescending(w => w.DateFollowed).Select(_mapper.Map<LabelDto>).ToList();

            return View(model);
        }



        [Authorize]
        public async Task<IActionResult> Comments()
        {
            var user = await _userManager.Users
                .Include(u => u.Comments)
                .SingleAsync(u => u.Id == GetUserId);

            var model = user.Comments.OrderByDescending(c=>c.DatePosted).Select(_mapper.Map<CommentDto>).ToList();

            

            return View(model);
        }



        [Authorize]
        public async Task<IActionResult> Addresses()
        {
            var user = await _userManager.Users
                .Include(u => u.Addresses)
                .SingleAsync(u => u.Id == GetUserId);

            var model = user.Addresses.OrderByDescending(a=>a.Id).Where(a=>a.IsActive).Select(_mapper.Map<AddressDto>).ToList();

            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> Orders()
        {
            var user = await _userManager.Users
                .Include(u => u.Orders)
                .SingleAsync(u => u.Id == GetUserId);

            var model = user.Orders.OrderByDescending(o=>o.OrderDate).Select(_mapper.Map<OrderViewModel>).ToList();

            return View(model);


        }
        private async Task AssignToRole(SimpleUser user)
        {
            bool adminExists = await _roleManager.RoleExistsAsync("Admin");
            if (!adminExists)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);

                var addAdmin = await _userManager.AddToRoleAsync(user, "Admin");
            }

            bool userExists = await _roleManager.RoleExistsAsync("User");
            if (!userExists)
            {
                var role = new IdentityRole();
                role.Name = "User";
                await _roleManager.CreateAsync(role);

            }
            var result1 = await _userManager.AddToRoleAsync(user, "User");
        }

        private string GetUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}