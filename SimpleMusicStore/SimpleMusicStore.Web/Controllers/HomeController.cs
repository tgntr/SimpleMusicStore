using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Services;
using SimpleMusicStore.Web.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SimpleMusicStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(
           UserManager<SimpleUser> userManager,
           SignInManager<SimpleUser> signInManager,
           SimpleDbContext context,
           RoleManager<IdentityRole> roleManager
           )
        {
            if (User != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }
        public IActionResult Index()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });


        }
    }
}
