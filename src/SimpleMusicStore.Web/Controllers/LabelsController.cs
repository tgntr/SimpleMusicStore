using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Web.Models.ViewModels;
using SimpleMusicStore.Web.Services;

namespace SimpleMusicStore.Web.Controllers
{
    public class LabelsController : Controller
    {
        private readonly LabelService _labelService;
        private readonly IMapper _mapper;



        public LabelsController(SimpleMusicStoreContext context, IMapper mapper)
        {
            _labelService = new LabelService(context);
            _mapper = mapper;
        }



        //public async Task<IActionResult> All(string orderBy = "")
        //{
        //    var userId = "";
        //    if (User != null)
        //    {
        //        userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    }
        //
        //    var model = (await _labelService.AllAsync(orderBy, userId)).Select(_mapper.Map<LabelViewModel>).ToList();
        //
        //    return View(model);
        //}

        public async Task<IActionResult> Details(int labelId)
        {
            var label = await _labelService.GetAsync(labelId);

            if (label is null)
            {
                return Redirect("/");
            }

            var model = _mapper.Map<LabelViewModel>(label);

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (label.Followers.Any(lu => lu.UserId == userId))
                {
                    model.IsFollowed = true;
                }
                model.Comments.ForEach(c => c.IsCreator = c.UserId == userId);
            }

            model.Records = model.Records.Where(r=>r.IsActive).OrderByDescending(r => r.DateAdded).ToList();
            model.Comments = model.Comments.OrderByDescending(c => c.DatePosted).ToList();

            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> Follow(int labelId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _labelService.FollowAsync(labelId, userId);

            return Redirect(Request.Headers["Referer"].ToString());
        }



        [Authorize]
        public async Task<IActionResult> Unfollow(int labelId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _labelService.UnfollowAsync(labelId, userId);

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}