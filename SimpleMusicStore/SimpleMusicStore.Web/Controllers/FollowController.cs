using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Services;

namespace SimpleMusicStore.Web.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        private FollowService _followService;
        private UserManager<SimpleUser> _userManager;

        private string _userId;


        public FollowController(UserManager<SimpleUser> userManager, SimpleDbContext context)
        {
            _followService = new FollowService(context);
            _userManager = userManager;
            _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public IActionResult WantRecord(int id)
        {
            _followService.WantRecord(id, _userId);

            //TODO return to user followed artists

            return Redirect("/");
        }

        public IActionResult UnWantRecord(int id)
        {
            _followService.UnwantRecord(id, _userId);

            //TODO return to user followed artists

            return Redirect("/");
        }

        public IActionResult FollowLabel(int id)
        {
            _followService.FollowLabel(id, _userId);

            //TODO return to user followed Labels

            return Redirect("/");
        }

        public IActionResult UnfollowLabel(int id)
        {
            _followService.UnfollowLabel(id, _userId);

            //TODO return to user followed Labels

            return Redirect("/");
        }
    }
}