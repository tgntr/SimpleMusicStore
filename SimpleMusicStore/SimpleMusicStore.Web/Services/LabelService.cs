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
        public LabelService(SimpleDbContext context)
        {
            _context = context;
        }

        public List<Label> All()
        {
            return _context.Labels
                .Include(l=>l.Followers)
                .Include(l=>l.Records)
                .ToList();
        }

        public Label GetLabel(int id)
        {
            return _context.Labels.Include(l=>l.Records).FirstOrDefault(a => a.Id == id);
        }
    }
}
