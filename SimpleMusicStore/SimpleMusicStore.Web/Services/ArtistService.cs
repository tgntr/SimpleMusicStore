using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Services
{
    public class ArtistService
    {
        private SimpleDbContext _context;
        public ArtistService(SimpleDbContext context)
        {
            _context = context;
        }

        public List<Artist> All()
        {
            return _context.Artists
                .Include(a=>a.Records)
                .Include(a=>a.Followers)
                .ToList();
        }

        public Artist GetArtist(int id)
        {
            return _context.Artists
                .Include(a => a.Records)
                .Include(a => a.Followers)
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
