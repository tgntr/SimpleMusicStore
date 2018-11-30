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
using SimpleMusicStore.Web.Areas.Admin.Utilities;
using SimpleMusicStore.Web.Utilities;

namespace SimpleMusicStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        public IActionResult Add()
        {
            

            return View();
        }

        [HttpPost]
        public IActionResult Add(AddRecordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var discogsId = DiscogsUtilities.GetDiscogsId(model.DiscogsUrl);
            if (!DiscogsUtilities.IsValidDiscogsId(discogsId))
            {
                return View();
            }

            return Redirect($"/admin/record/preview/{discogsId}");
        }

        public IActionResult Preview(int id)
        {
            if (!DiscogsUtilities.IsValidDiscogsId(id))
            {
                return RedirectToAction("Add");
            }

            Record recordDto;

            return View();
        }

        [HttpPost]
        public IActionResult Preview(PreviewRecordBindingModel model)
        {
            return Redirect("/");
        }

        
    }
}