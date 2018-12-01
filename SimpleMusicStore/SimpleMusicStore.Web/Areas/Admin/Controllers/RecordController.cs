using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Areas.Admin.Models;
using SimpleMusicStore.Web.Areas.Admin.Models.RecordDtos;
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
           RoleManager<IdentityRole> roleManager,
           SimpleDbContext context,
           IMapper mapper)
            : base(userManager, signInManager, roleManager)
        {
            _recordService = new RecordService(context, mapper);
        }

        public IActionResult Add()
        {
            _recordService.AddRecord(DiscogsUtilities.Get<RecordDto>(950220), 20);

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
            return Redirect($"/admin/record/preview/{discogsId}");
        }

        public IActionResult Preview(long id)
        {
            if (!DiscogsUtilities.IsValidDiscogsId(id))
            {
                return RedirectToAction("Add");
            }

            var recordDto = DiscogsUtilities.Get<RecordDto>(id);

            return View(recordDto);
        }

        [HttpPost]
        public IActionResult Preview(long id, PreviewRecordBindingModel model)
        {
            if (!DiscogsUtilities.IsValidDiscogsId(id))
            {
                return RedirectToAction("Add");
            }

            var recordDto = DiscogsUtilities.Get<RecordDto>(id);
            _recordService.AddRecord(recordDto, model.Price);

            return Redirect("/");
        }

        
    }
}