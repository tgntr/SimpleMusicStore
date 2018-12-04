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
        private string _userId;

        public CommentService(SimpleDbContext context, string userId)
        {
            _context = context;
            _recordService = new RecordService(context, userId);
            _artistService = new ArtistService(context, userId);
            _labelService = new LabelService(context, userId);
            _userId = userId;
        }

        internal void AddComment<T>(int id, string content)
        {
            Comment comment = new Comment { UserId = _userId, Content = content };
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
