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

        public CommentService(SimpleDbContext context, string userId)
            :base(context, userId)
        {
            _recordService = new RecordService(context, userId);
            _artistService = new ArtistService(context, userId);
            _labelService = new LabelService(context, userId);
        }

        internal void AddComment<T>(int targetId, string content)
        {
            Comment comment = new Comment { UserId = _userId, Content = content };
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

        

        internal void RemoveComment(int commentId, bool isAdmin = false)
        {

            var comment = _context.Comments.FirstOrDefault();

            if (comment is null || (comment.UserId != _userId && !isAdmin))
            {
                return;
            }

            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }



        private Comment GetComment(int commentId) => _context.Comments.FirstOrDefault(c => c.Id == commentId);
    }
}
