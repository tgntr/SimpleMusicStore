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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _labelService = new LabelService(context, userId);
            _mapper = mapper;
            _referrerUrl = Request.Headers["Referer"].ToString();
        }



        public IActionResult All(string orderBy = "")
        {
            List<LabelViewModel> labels = _labelService.All(orderBy).Select(_mapper.Map<LabelViewModel>).ToList();

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
            _labelService.FollowLabel(id);

            return Redirect(_referrerUrl);
        }



        [Authorize]
        public IActionResult UnfollowLabel(int id)
        {
            _labelService.UnfollowLabel(id);

            return Redirect(_referrerUrl);
        }
    }
}