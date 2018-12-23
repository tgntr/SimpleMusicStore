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
        private readonly string _referrerUrl;
        private readonly IMapper _mapper;



        public LabelsController(SimpleDbContext context, IMapper mapper)
        {
            _labelService = new LabelService(context);
            _mapper = mapper;
            _referrerUrl = Request.Headers["Referer"].ToString();
        }



        public async Task<IActionResult> All(string orderBy = "")
        {
            var userId = "";
            if (User != null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var labels = (await _labelService.All(orderBy, userId)).Select(_mapper.Map<LabelViewModel>).ToList();

            return View(labels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var label = await _labelService.GetLabel(id);

            if (label is null)
            {
                return RedirectToAction("All");
            }

            var viewModel = _mapper.Map<LabelViewModel>(label);

            return View(viewModel);
        }


        [Authorize]
        public async Task<IActionResult> FollowLabel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _labelService.FollowLabel(id, userId);

            return Redirect(_referrerUrl);
        }



        [Authorize]
        public async Task<IActionResult> UnfollowLabel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _labelService.UnfollowLabel(id, userId);

            return Redirect(_referrerUrl);
        }
    }
}