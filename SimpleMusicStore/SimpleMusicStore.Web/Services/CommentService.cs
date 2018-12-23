using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
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

        public CommentService(SimpleDbContext context)
            :base(context)
        {
            _recordService = new RecordService(context);
            _artistService = new ArtistService(context);
            _labelService = new LabelService(context);
        }

        internal async Task AddComment<T>(int targetId, string content, string userId)
        {
            Comment comment = new Comment { UserId = userId, Content = content };
            if (typeof(T) == typeof(Record))
            {
                if (!await _recordService.IsValidRecordId(targetId))
                {
                    return;
                }

                comment.RecordId = targetId;
            }
            else if (typeof(T) == typeof(Artist))
            {
                if (!await _artistService.IsValidArtistId(targetId))
                { 
                    return;
                }

                comment.ArtistId = targetId;
            }
            else if (typeof(T) == typeof(Label))
            {
                if (!await _labelService.IsValidLabelId(targetId))
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

        

        internal async Task RemoveComment(int commentId, string userId, bool isAdmin)
        {

            var comment = await _context.Comments.FirstOrDefaultAsync();

            if (comment is null || (comment.UserId != userId && !isAdmin))
            {
                return;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }



        private async Task<Comment> GetComment(int commentId) => await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
    }
}
