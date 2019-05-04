using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Data;
using SimpleMusicStore.Data.Models;
using SimpleMusicStore.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Services
{
    internal class CommentService : Service
    {
        private RecordService _recordService;
        private ArtistService _artistService;
        private LabelService _labelService;

        public CommentService(SimpleMusicStoreContext context)
            :base(context)
        {
            _recordService = new RecordService(context);
            _artistService = new ArtistService(context);
            _labelService = new LabelService(context);
        }

        internal async Task AddAsync<T>(int targetId, string content, string userId)
        {
            Comment comment = new Comment { UserId = userId, Content = content };
            if (typeof(T) == typeof(Record))
            {
                if (!await _recordService.IsValidIdAsync(targetId))
                {
                    return;
                }

                comment.RecordId = targetId;
            }
            else if (typeof(T) == typeof(Artist))
            {
                if (!await _artistService.IsValidIdAsync(targetId))
                { 
                    return;
                }

                comment.ArtistId = targetId;
            }
            else if (typeof(T) == typeof(Label))
            {
                if (!await _labelService.IsValidIdAsync(targetId))
                {
                    return;
                }

                comment.LabelId = targetId;
            }
            else
            {
                return;
            }

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        

        internal async Task RemoveAsync(int commentId, string userId)
        {

            var comment = await _context.Comments.FirstOrDefaultAsync(c=>c.Id == commentId);

            if (comment is null || comment.UserId != userId)
            {
                return;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }



        private async Task<Comment> GetAsync(int commentId) => await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
    }
}
