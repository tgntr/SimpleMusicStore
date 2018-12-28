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
                .SingleAsync(u => u.Id == GetUserId);

            var wantlist = user.Wantlist.Select(_mapper.Map<RecordViewModel>).ToList();

            return View(wantlist);
        }



        [Authorize]
        public async Task<IActionResult> FollowedArtists()
        {
            var user = await _userManager.Users
                .Include(u => u.FollowedArtists)
                   .ThenInclude(al => al.Artist)
                .SingleAsync(u => u.Id == GetUserId);

            var followedArtists = user.FollowedArtists.Select(_mapper.Map<ArtistViewModel>).ToList();

            return View(followedArtists);
        }



        [Authorize]
        public async Task<IActionResult> FollowedLabels()
        {
            var user = await _userManager.Users
                .Include(u => u.FollowedLabels)
                    .ThenInclude(fl=>fl.Label)
                .SingleAsync(u => u.Id == GetUserId);

            var followedLabels = user.FollowedLabels.Select(_mapper.Map<LabelViewModel>).ToList();

            return View(followedLabels);
        }



        [Authorize]
        public async Task<IActionResult> Comments()
        {
            var user = await _userManager.Users
                .Include(u => u.Comments)
                .SingleAsync(u => u.Id == GetUserId);

            var comments = user.Comments.Select(_mapper.Map<CommentDto>).ToList();

            return View(comments);
        }



        [Authorize]
        public async Task<IActionResult> Addresses()
        {
            var user = await _userManager.Users
                .Include(u => u.Addresses)
                .SingleAsync(u => u.Id == GetUserId);

            var addresses = user.Addresses.Select(_mapper.Map<AddressDto>).ToList();

            return View(addresses);
        }


        [Authorize]
        public async Task<IActionResult> Orders()
        {
            var user = await _userManager.Users
                .Include(u => u.Orders)
                .SingleAsync(u => u.Id == GetUserId);

            var addresses = user.Orders.Select(_mapper.Map<OrderViewModel>).ToList();

            return View(addresses);


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