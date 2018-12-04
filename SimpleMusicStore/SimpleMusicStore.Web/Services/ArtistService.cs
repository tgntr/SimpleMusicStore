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
        private string _userId;

        internal ArtistService(SimpleDbContext context, string userId)
        {
            _context = context;
            _userId = userId;
        }

        private List<Artist> All()
        {
            return _context.Artists
                .Include(a=>a.Records)
                    .ThenInclude(r=>r.Orders)
                        .ThenInclude(o=>o.Order)
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
            else if (orderBy == "popularity" || (orderBy == "recommended" && _userId == ""))
            {
                artists = All().OrderByDescending(a => a.Followers.Count() + a.Records.Sum(r => r.Orders.Count())).ToList();
            }
            else if (orderBy == "recommended")
            {
                artists = All().OrderByDescending(a =>
                {
                    if (a.Followers.Any(f => f.UserId == _userId))
                    {
                        return -1;
                    }
                    var labelOrders = a.Records.Where(r => r.Orders.Any(o => o.Order.UserId == _userId)).Count();

                    return labelOrders;
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



        internal void FollowArtist(int artistId)
        {
            if (!IsValidArtistId(artistId))
            {
                return;
            }

            var artistUser = new ArtistUser { ArtistId = artistId, UserId = _userId };

            if (_context.ArtistUsers.Contains(artistUser))
            {
                return;
            }

            _context.ArtistUsers.Add(artistUser);
            _context.SaveChanges();
        }

        internal void UnfollowArtist(int artistId)
        {
            if (!IsValidArtistId(artistId))
            {
                return;
            }

            var artistUser = new ArtistUser { ArtistId = artistId, UserId = _userId };

            if (!_context.ArtistUsers.Contains(artistUser))
            {
                return;
            }

            _context.ArtistUsers.Remove(artistUser);
            _context.SaveChanges();
        }
    }
}
