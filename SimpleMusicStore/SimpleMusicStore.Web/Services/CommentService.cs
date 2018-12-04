using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Services
{
    public class CommentService
    {
        private SimpleDbContext _context;
        private RecordService _recordService;
        private ArtistService _artistService;
        private LabelService _labelService;

        public CommentService(SimpleDbContext context)
        {
            _context = context;
            _recordService = new RecordService(context);
            _artistService = new ArtistService(context);
            _labelService = new LabelService(context);
        }

        internal void AddComment<T>(int id, string userId, string content)
        {
            Comment comment = new Comment { UserId = userId, Content = content };
            if (typeof(T) == typeof(Record))
            {
                if (!_recordService.IsValidRecordId(id))
                {
                    return;
                }

                comment.RecordId = id;
            }
            else if (typeof(T) == typeof(Artist))
            {
                if (!_artistService.IsValidArtistId(id))
                { 
                    return;
                }

                comment.ArtistId = id;
            }
            else if (typeof(T) == typeof(Label))
            {
                if (!_labelService.IsValidLabelId(id))
                {
                    return;
                }

                comment.LabelId = id;
            }
            else
            {
                return;
            }

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        

        internal void RemoveComment(int commentId, string userId, bool isAdmin = false)
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
