using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Services
{
    internal class ArtistService
    {
        private SimpleDbContext _context;

        internal ArtistService(SimpleDbContext context)
        {
            _context = context;
        }

        private List<Artist> All()
        {
            return _context.Artists
                .Include(a=>a.Records)
                .Include(a=>a.Followers)
                .ToList();
        }

        internal List<Artist> All(string orderBy)
        {
            List<Artist> artists;
            if (orderBy == "alphabetically")
            {
                artists = All().OrderBy(a => a.Name).ToList();
            }
            else if (orderBy == "popularity")
            {
                artists = All().OrderByDescending(a => a.Followers.Count()).ToList();
            }
            else
            {
                artists = All().ToList();
            }

            return artists;
        }

        internal Artist GetArtist(int id)
        {
            return _context.Artists
                .Include(a => a.Records)
                .Include(a => a.Followers)
                .Include(a => a.Comments)
                    .ThenInclude(c=>c.User.UserName)
                .FirstOrDefault(a => a.Id == id);
        }

        internal bool IsValidArtistId(int id)
        {
            return _context.Artists.Any(l => l.Id == id);
        }
    }
}
