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
        private readonly string _referrerUrl;
        private readonly IMapper _mapper;

        

        public ArtistsController(SimpleDbContext context, IMapper mapper)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _artistService = new ArtistService(context, userId);
            _mapper = mapper;
            _referrerUrl = Request.Headers["Referer"].ToString();
        }



        public IActionResult All(string orderBy = "")
        {
            List<ArtistViewModel> artists = _artistService.All(orderBy).Select(_mapper.Map<ArtistViewModel>).ToList();

            return View(artists);
        }

        public IActionResult Details(int id)
        {
            var artist = _artistService.GetArtist(id);

            if (artist is null)
            {
                return RedirectToAction("All");
            }

            var viewModel = _mapper.Map<ArtistViewModel>(artist);

            return View(viewModel);
        }



        [Authorize]
        public IActionResult Follow(int id)
        {
            _artistService.FollowArtist(id);

            return Redirect(_referrerUrl);
        }


        [Authorize]
        public IActionResult Unfollow(int id)
        {
            _artistService.UnfollowArtist(id);

            return Redirect(_referrerUrl);
        }

    }
}