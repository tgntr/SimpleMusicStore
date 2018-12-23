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
    internal class ArtistService : Service
    {

        internal ArtistService(SimpleDbContext context)
            :base(context)
        {
        }

        //private async Task<List<Artist>> All()
        //{
        //    return await Task.Run(() => _context.Artists
        //        .Include(a => a.Records)
        //            .ThenInclude(r => r.Orders)
        //                .ThenInclude(o => o.Order)
        //        .Include(a => a.Followers)
        //        .ToList());
        //}


        private async Task<List<Artist>> All()
        {
            var artists = await _context.Artists
                .Include(a => a.Records)
                    .ThenInclude(r => r.Orders)
                        .ThenInclude(o => o.Order)
                .Include(a => a.Followers)
                .ToListAsync();
            return artists;
        }

        internal async Task<List<Artist>> All(string orderBy, string userId = null)
        {
            var artists = await All();
            if (orderBy == "alphabetically")
            {
                artists = artists.OrderBy(a => a.Name).ToList();
            }
            else if (orderBy == "popularity" || (orderBy == "recommended" && userId == null))
            {
                artists = artists.OrderByDescending(a => a.Followers.Count() + a.Records.Sum(r => (r.Orders.Count() * 2) + r.WantedBy.Count())).ToList();
            }
            else if (orderBy == "recommended")
            {
                artists = artists.OrderByDescending(a =>
                {
                    if (a.Followers.Any(f => f.UserId == userId))
                    {
                        return -1;
                    }
                    var artistOrder = a.Records.Where(r => r.Orders.Any(o => o.Order.UserId == userId)).Count();

                    return artistOrder;
                })
                .ToList();
            }
            else
            {
                artists = artists.ToList();
            }

            return artists;
        }

        internal async Task<Artist> GetArtist(int artistId)
        {
            return await _context.Artists
                .Include(a => a.Records)
                    .ThenInclude(r=>r.Label)
                .Include(a => a.Followers)
                .Include(a => a.Comments)
                    .ThenInclude(c=>c.User.UserName)
                .FirstOrDefaultAsync(a => a.Id == artistId);
        }

        internal async Task<bool> IsValidArtistId(int artistId)
        {
            return await _context.Artists.AnyAsync(l => l.Id == artistId);
        }



        internal async Task FollowArtist(int artistId, string userId)
        {
            if (!await IsValidArtistId(artistId))
            {
                return;
            }

            var artistUser = new ArtistUser { ArtistId = artistId, UserId = userId };

            if (_context.ArtistUsers.Contains(artistUser))
            {
                return;
            }

            await _context.ArtistUsers.AddAsync(artistUser);
            await _context.SaveChangesAsync();
        }

        internal async Task UnfollowArtist(int artistId, string userId)
        {
            if (!await IsValidArtistId(artistId))
            {
                return;
            }

            var artistUser = new ArtistUser { ArtistId = artistId, UserId = userId };

            if (!_context.ArtistUsers.Contains(artistUser))
            {
                return;
            }

            _context.ArtistUsers.Remove(artistUser);
            await _context.SaveChangesAsync();
        }
        
    }
}
