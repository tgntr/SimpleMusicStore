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
using SimpleMusicStore.Web.Models.ViewModels;
using SimpleMusicStore.Web.Services;

namespace SimpleMusicStore.Web.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private CommentService _commentService;
        
        

        public CommentsController(SimpleDbContext context)
        {
            _commentService = new CommentService(context);
            
            
        }
        


        [HttpPost]
        public async Task<IActionResult> CommentRecord(int recordId, RecordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect($"/records/details?recordId={recordId}");
            }
            
            await _commentService.AddComment<Record>(recordId, model.Comment, GetUserId);

            return Redirect($"/records/details?recordId={recordId}");
        }



        [HttpPost]
        public async Task<IActionResult> CommentArtist(int artistId, ArtistViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect($"/artists/details?artistId={artistId}");
            }

            await _commentService.AddComment<Artist>(model.Id, model.Comment, GetUserId);

            return Redirect($"/artists/details?artistId={artistId}");
        }

        [HttpPost]
        public async Task<IActionResult> CommentLabel(int labelId, LabelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect($"/labels/details?labeId={labelId}");
            }
            
            await _commentService.AddComment<Label>(model.Id, model.Comment, GetUserId);

            return Redirect($"/labels/details?labeId={labelId}");
        }

        public async Task<IActionResult> RemoveComment(int commentId)
        {
            var isAdmin = User.IsInRole("Admin");
            await _commentService.RemoveComment(commentId, GetUserId, isAdmin);

            return Redirect("/");
        }

        private string GetUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}