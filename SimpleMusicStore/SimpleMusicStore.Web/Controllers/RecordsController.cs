using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Models.ViewModels;
using SimpleMusicStore.Web.Services;

namespace SimpleMusicStore.Web.Controllers
{
    public class RecordsController : Controller
    {
        private readonly RecordService _recordService;
        private readonly IMapper _mapper;
        private readonly UserManager<SimpleUser> _userManager;





        public RecordsController(SimpleDbContext context, IMapper mapper, UserManager<SimpleUser> userManager)
        {
            _recordService = new RecordService(context);
            _mapper = mapper;
            _userManager = userManager;
        }




        public async Task<IActionResult> All()
        {
            var userId = "";
            if (User != null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            var records = (await _recordService.All(userId)).Select(_mapper.Map<RecordViewModel>).ToList();
            var allRecordsViewModel = new AllRecordsViewModel { Records = records };

            return View(allRecordsViewModel);
        }




        [HttpPost]
        public async Task<IActionResult> All(AllRecordsViewModel model)
        {
            var userId = "";
            if (User != null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            var records = (await _recordService.All(model.Sort, userId, model.SelectedGenres)).Select(_mapper.Map<RecordViewModel>).ToList();
            model.Records = records;

            return View(model);
        }




        public async Task<IActionResult> Details(int recordId)
        {
            var record = await _recordService.GetRecordAsync(recordId);

            if (record is null)
            {
                return RedirectToAction("All");
            }

            var viewModel = _mapper.Map<RecordViewModel>(record);

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (record.WantedBy.Any(ru=> ru.UserId == userId))
                {
                    viewModel.IsFollowed = true;
                }
            }
            return View(viewModel);
        }



        [Authorize]
        public async Task<IActionResult> AddToWantlist(int recordId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _recordService.AddToWantlist(recordId, userId);;
            return Redirect("/records/details?recordId=" + recordId);
        }



        [Authorize]
        public async Task<IActionResult> RemoveFromWantList(int recordId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _recordService.RemoveFromWantlist(recordId, userId);
            return Redirect("/records/details?recordId=" + recordId);
        }
    }
}