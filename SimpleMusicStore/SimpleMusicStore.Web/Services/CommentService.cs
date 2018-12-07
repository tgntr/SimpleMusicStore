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

        internal void AddComment<T>(int targetId, string content, string userId)
        {
            Comment comment = new Comment { UserId = userId, Content = content };
            if (typeof(T) == typeof(Record))
            {
                if (!_recordService.IsValidRecordId(targetId))
                {
                    return;
                }

                comment.RecordId = targetId;
            }
            else if (typeof(T) == typeof(Artist))
            {
                if (!_artistService.IsValidArtistId(targetId))
                { 
                    return;
                }

                comment.ArtistId = targetId;
            }
            else if (typeof(T) == typeof(Label))
            {
                if (!_labelService.IsValidLabelId(targetId))
                {
                    return;
                }

                comment.LabelId = targetId;
            }
            else
            {
                return;
            }

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        

        internal void RemoveComment(int commentId, string userId, bool isAdmin)
        {

            var comment = _context.Comments.FirstOrDefault();

            if (comment is null || (comment.UserId != userId && !isAdmin))
            {
                return;
            }

            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }



        private Comment GetComment(int commentId) => _context.Comments.FirstOrDefault(c => c.Id == commentId);
    }
}
