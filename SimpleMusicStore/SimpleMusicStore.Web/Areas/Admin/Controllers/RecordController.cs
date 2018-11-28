using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Areas.Admin.Models;
using SimpleMusicStore.Web.Areas.Admin.Services;
using SimpleMusicStore.Web.Controllers;

namespace SimpleMusicStore.Web.Areas.Admin.Controllers
{
    public class RecordController : BaseController
    {
        private RecordService _recordService;
        public RecordController(
           UserManager<SimpleUser> userManager,
           SignInManager<SimpleUser> signInManager,
           SimpleDbContext context,
           RoleManager<IdentityRole> roleManager
           )
            : base(userManager, signInManager, context, roleManager)
        {
            _recordService = new RecordService(context);
        }
        [HttpGet("/admin/record/add")]
        public IActionResult Add()
        {
            _recordService.ImportFromDiscogs("https://www.discogs.com/Soul-Capsule-Overcome/master/484910");
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddRecordBindingModel model)
        {
            return Redirect("/");
        }
    }
}