using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Web.Services;
using System.Security.Claims;
using SimpleMusicStore.Web.Models.ViewModels;
using AutoMapper;
using SimpleMusicStore.Web.Models;

namespace SimpleMusicStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly RecordService _recordService;
        private readonly LabelService _labelService;
        private readonly ArtistService _artistService;
        private readonly IMapper _mapper;


        public HomeController(
           SimpleMusicStoreContext context,
           IMapper mapper
           )
        {
            _recordService = new RecordService(context);
            _labelService = new LabelService(context);
            _artistService = new ArtistService(context);
            _mapper = mapper;
        }



        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.RecommendedRecords = (await _recordService.AllAsync("recommended", userId)).Select(_mapper.Map < RecordViewModel>).Take(10).ToList();
            }

            model.MostPopularRecords = (await _recordService.AllAsync("popularity")).Select(_mapper.Map<RecordViewModel>).Take(10).ToList();
            model.NewArrivals = (await _recordService.AllAsync("newest")).Select(_mapper.Map<RecordViewModel>).Take(10).ToList();
           // homeViewModel.MostPopularArtists = (await _artistService.All("popularity")).Select(_mapper.Map<ArtistViewModel>).Take(5).ToList();
           // homeViewModel.MostPopularLabels = (await _labelService.All("popularity")).Select(_mapper.Map<LabelViewModel>).Take(5).ToList();

            return View(model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });


        }
    }
}
