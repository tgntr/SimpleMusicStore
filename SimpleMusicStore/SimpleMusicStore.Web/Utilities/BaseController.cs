using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;

namespace SimpleMusicStore.Web.Utilities
{
    public abstract class BaseController : Controller
    {
        protected readonly SignInManager<SimpleUser> _signInManager;
        protected readonly UserManager<SimpleUser> _userManager;
        protected readonly SimpleDbContext _context;
        protected readonly RoleManager<IdentityRole> _roleManager;

        public BaseController(
           UserManager<SimpleUser> userManager,
           SignInManager<SimpleUser> signInManager,
           SimpleDbContext context,
           RoleManager<IdentityRole> roleManager
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }
    }
}