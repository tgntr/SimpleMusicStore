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

        

        public ArtistsController(SimpleDbContext context, IMapper mapper)
        {
            _artistService = new ArtistService(context);
            _mapper = mapper;
        }



        public async Task<IActionResult> All(string orderBy = "")
        {
            var userId = "";
            if (User != null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            List<ArtistViewModel> model = (await _artistService.All(orderBy, userId)).Select(_mapper.Map<ArtistViewModel>).ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int artistId)
        {
            var artist = await _artistService.GetArtist(artistId);

            if (artist is null)
            {
                return RedirectToAction("All");
            }

            var model = _mapper.Map<ArtistViewModel>(artist);

            return View(model);
        }



        [Authorize]
        public async Task<IActionResult> Follow(int artistId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _artistService.FollowArtist(artistId, userId);

            return Redirect("/artists/details?artistId=" + artistId);
        }


        [Authorize]
        public async Task<IActionResult> Unfollow(int artistId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _artistService.UnfollowArtist(artistId, userId);
        
            return Redirect("/artists/details?artistId=" + artistId);
        }

    }
}