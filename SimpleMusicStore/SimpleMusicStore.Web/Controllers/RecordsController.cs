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
    public class RecordsController : Controller
    {
        private readonly RecordService _recordService;
        private readonly string _referrerUrl;
        private readonly IMapper _mapper;




        public RecordsController(SimpleDbContext context, IMapper mapper)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _recordService = new RecordService(context, mapper, userId);
            _mapper = mapper;
            _referrerUrl = Request.Headers["Referer"].ToString();
        }




        public IActionResult All(string orderBy = "")
        {
            var records = _recordService.All(orderBy).Select(_mapper.Map<RecordViewModel>).ToList();
            var allRecordsViewModel = new AllRecordsViewModel { Records = records };

            return View(allRecordsViewModel);
        }




        [HttpPost]
        public IActionResult All(AllRecordsViewModel model, string orderBy = "")
        {
            var records = _recordService.All(orderBy, model.SelectedGenres).Select(_mapper.Map<RecordViewModel>).ToList();
            model.Records = records;

            return View(model);
        }




        public IActionResult Details(int id)
        {
            var record = _recordService.GetRecord(id);

            if (record is null)
            {
                return RedirectToAction("All");
            }

            var viewModel = _mapper.Map<RecordViewModel>(record);

            return View(viewModel);
        }



        [Authorize]
        public IActionResult AddToWantlist(int id)
        {
            _recordService.AddToWantlist(id);

            return Redirect(_referrerUrl);
        }



        [Authorize]
        public IActionResult RemoveFromWantList(int id)
        {
            _recordService.RemoveFromWantlist(id);

            return Redirect(_referrerUrl);
        }
    }
}