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


        private List<Artist> All()
        {

            return _context.Artists
                .Include(a=>a.Records)
                    .ThenInclude(r=>r.Orders)
                        .ThenInclude(o=>o.Order)
                .Include(a=>a.Followers)
                .ToList();
        }

        internal List<Artist> All(string orderBy, string userId = null)
        {
            List<Artist> artists;
            if (orderBy == "alphabetically")
            {
                artists = All().OrderBy(a => a.Name).ToList();
            }
            else if (orderBy == "popularity" || (orderBy == "recommended" && userId == null))
            {
                artists = All().OrderByDescending(a => a.Followers.Count() + a.Records.Sum(r => (r.Orders.Count() * 2) + r.WantedBy.Count())).ToList();
            }
            else if (orderBy == "recommended")
            {
                artists = All().OrderByDescending(a =>
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
                artists = All().ToList();
            }

            return artists;
        }

        internal Artist GetArtist(int artistId)
        {
            return _context.Artists
                .Include(a => a.Records)
                    .ThenInclude(r=>r.Label)
                .Include(a => a.Followers)
                .Include(a => a.Comments)
                    .ThenInclude(c=>c.User.UserName)
                .FirstOrDefault(a => a.Id == artistId);
        }

        internal bool IsValidArtistId(int artistId)
        {
            return _context.Artists.Any(l => l.Id == artistId);
        }



        internal async Task FollowArtist(int artistId, string userId)
        {
            if (!IsValidArtistId(artistId))
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
            if (!IsValidArtistId(artistId))
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

        internal List<Artist> AllFollowed(string userId)
        {
            return All().Where(a => a.Followers.Any(f => f.UserId == userId)).ToList();
        }
    }
}
