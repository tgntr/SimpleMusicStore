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
    public class ArtistsController : Controller
    {
        private readonly ArtistService _artistService;
        private readonly IMapper _mapper;

        

        public ArtistsController(SimpleMusicStoreContext context, IMapper mapper)
        {
            _artistService = new ArtistService(context);
            _mapper = mapper;
        }



        //public async Task<IActionResult> All(string sort = "")
        //{
        //    var userId = "";
        //    if (User != null)
        //    {
        //        userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    }
        //    var model = (await _artistService.AllAsync(sort, userId)).Select(_mapper.Map<ArtistViewModel>).ToList();
        //
        //    return View(model);
        //}

        public async Task<IActionResult> Details(int artistId)
        {
            var artist = await _artistService.GetAsync(artistId);

            if (artist is null)
            {
                return Redirect("/");
            }

            var model = _mapper.Map<ArtistViewModel>(artist);

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (artist.Followers.Any(lu => lu.UserId == userId))
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
        public async Task<IActionResult> Follow(int artistId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _artistService.FollowAsync(artistId, userId);

            return Redirect(Request.Headers["Referer"].ToString());
        }


        [Authorize]
        public async Task<IActionResult> Unfollow(int artistId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _artistService.UnfollowAsync(artistId, userId);
        
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}