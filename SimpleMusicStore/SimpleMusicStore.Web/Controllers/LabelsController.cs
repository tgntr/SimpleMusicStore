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



        public IActionResult All(string orderBy = "")
        {
            var userId = "";
            if (User != null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            List<LabelViewModel> labels = _labelService.All(orderBy, userId).Select(_mapper.Map<LabelViewModel>).ToList();

            return View(labels);
        }

        public IActionResult Details(int id)
        {
            var label = _labelService.GetLabel(id);

            if (label is null)
            {
                return RedirectToAction("All");
            }

            var viewModel = _mapper.Map<LabelViewModel>(label);

            return View(viewModel);
        }


        [Authorize]
        public IActionResult FollowLabel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _labelService.FollowLabel(id, userId);

            return Redirect(_referrerUrl);
        }



        [Authorize]
        public IActionResult UnfollowLabel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _labelService.UnfollowLabel(id, userId);

            return Redirect(_referrerUrl);
        }

        [Authorize]
        public IActionResult Followed()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var labels = _labelService.AllFollowed(userId).Select(_mapper.Map<LabelViewModel>).ToList();

            return View(labels);
        }
    }
}