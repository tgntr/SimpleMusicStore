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



        public async Task<IActionResult> All(string sort = "newest", List<string> selectedGenres = null, List<string> selectedFormats = null, string search = "")
        {
            var userId = "";
            if (User != null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            var records = (await _recordService.AllAsync(sort, userId, selectedGenres, selectedFormats, search)).Select(_mapper.Map<RecordViewModel>).ToList();
            var model = new AllRecordsViewModel
            {
                Records = records,
                Sort = sort,
                AllGenres = _recordService.GetAllGenres(),
                SelectedGenres = selectedGenres,
                AllFormats = _recordService.GetAllFormats(),
                SelectedFormats = selectedFormats,
                Search = search
            };

            return View(model);
        }




        public async Task<IActionResult> Details(int recordId)
        {
            var record = await _recordService.GetAsync(recordId);

            if (record is null)
            {
                return RedirectToAction("All");
            }

            var model = _mapper.Map<RecordViewModel>(record);

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (record.WantedBy.Any(ru=> ru.UserId == userId))
                {
                    model.IsFollowed = true;
                }

                model.Comments.ForEach(c => c.IsCreator = c.UserId == userId);
            }

            model.Comments = model.Comments.OrderByDescending(c => c.DatePosted).ToList();

            return View(model);


        }



        [Authorize]
        public async Task<IActionResult> AddToWantlist(int recordId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _recordService.AddToWantlistAsync(recordId, userId);;
            return Redirect(Request.Headers["Referer"].ToString());
        }



        [Authorize]
        public async Task<IActionResult> RemoveFromWantList(int recordId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _recordService.RemoveFromWantlistAsync(recordId, userId);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}