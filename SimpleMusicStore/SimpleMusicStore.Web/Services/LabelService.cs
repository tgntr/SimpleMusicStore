using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Services
{
    internal class LabelService
    {
        private SimpleDbContext _context;

        internal LabelService(SimpleDbContext context)
        {
            _context = context;
        }

        private List<Label> All()
        {
            return _context.Labels
                .Include(l=>l.Followers)
                .ToList();
        }

        internal List<Label> All(string orderBy)
        {

        }

        internal Label GetLabel(int id)
        {
            return _context.Labels
                .Include(l=>l.Records)
                .Include(l=>l.Comments)
                    .ThenInclude(c=>c.User.UserName)
                .FirstOrDefault(a => a.Id == id);
        }

        internal bool IsValidLabelId(int id)
        {
            return _context.Labels.Any(l => l.Id == id);
        }
    }
}
