using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Data.Models;
using SimpleMusicStore.Web.Models.ViewModels;
using SimpleMusicStore.Web.Services;

namespace SimpleMusicStore.Web.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private CommentService _commentService;
        
        

        public CommentsController(SimpleMusicStoreContext context)
        {
            _commentService = new CommentService(context);
        }
        


        [HttpPost]
        public async Task<IActionResult> CommentRecord(int recordId, RecordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            
            await _commentService.AddAsync<Record>(recordId, model.Comment, GetUserId);

            return Redirect($"/records/details?recordId={recordId}");
        }



        [HttpPost]
        public async Task<IActionResult> CommentArtist(int artistId, ArtistViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect($"/artists/details?artistId={artistId}");
            }

            await _commentService.AddAsync<Artist>(artistId, model.Comment, GetUserId);

            return Redirect($"/artists/details?artistId={artistId}");
        }

        [HttpPost]
        public async Task<IActionResult> CommentLabel(int labelId, LabelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect($"/labels/details?labelId={labelId}");
            }
            
            await _commentService.AddAsync<Label>(labelId, model.Comment, GetUserId);

            return Redirect($"/labels/details?labelId={labelId}");
        }

        public async Task<IActionResult> Remove(int commentId)
        {
            await _commentService.RemoveAsync(commentId, GetUserId);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        private string GetUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}