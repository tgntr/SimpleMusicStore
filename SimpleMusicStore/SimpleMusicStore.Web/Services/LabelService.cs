using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMusicStore.Web.Services
{
    internal class LabelService : Service
    {
        internal LabelService(SimpleDbContext context)
            : base(context)
        {
        }

       


        internal List<Label> All(string orderBy, string userId = null)
        {
            List<Label> labels;
            if (orderBy == "alphabetically")
            {
                labels = All().OrderBy(a => a.Name).ToList();
            }
            else if (orderBy == "popularity" || (orderBy == "recommended" && userId == null))
            {
                labels = All().OrderByDescending(l => l.Followers.Count() + l.Records.Sum(r=>(r.Orders.Count() * 2) + r.WantedBy.Count())).ToList();
            }
            else if (orderBy == "recommended")
            {
                labels = All().OrderByDescending(l=>
                {
                    if (l.Followers.Any(f => f.UserId == userId))
                    {
                        return -1;
                    }
                    var labelOrders = l.Records.Where(r => r.Orders.Any(o => o.Order.UserId == userId)).Count();

                    return labelOrders;
                })
                .ToList();
            }
            else
            {
                labels = All().ToList();
            }

            return labels;
        }



        private List<Label> All()
        {
            return _context.Labels
                .Include(l => l.Followers)
                .Include(l => l.Records)
                    .ThenInclude(r => r.Orders)
                        .ThenInclude(o => o.Order)
                .ToList();
        }



        internal Label GetLabel(int labelId)
        {
            return _context.Labels
                .Include(l=>l.Records)
                    .ThenInclude(r => r.Artist)
                .Include(l=>l.Comments)
                    .ThenInclude(c=>c.User.UserName)
                .FirstOrDefault(a => a.Id == labelId);
        }



        internal bool IsValidLabelId(int labelId)
        {
            return _context.Labels.Any(l => l.Id == labelId);
        }



        internal void FollowLabel(int labelId, string userId)
        {
            if (!IsValidLabelId(labelId))
            {
                return;
            }
            var labelUser = new LabelUser { LabelId = labelId, UserId = userId };

            if (_context.LabelUsers.Contains(labelUser))
            {
                return;
            }

            _context.LabelUsers.Add(labelUser);
            _context.SaveChanges();
        }



        internal void UnfollowLabel(int labelId, string userId)
        {
            if (!IsValidLabelId(labelId))
            {
                return;
            }

            var labelUser = new LabelUser { LabelId = labelId, UserId = userId };

            if (!_context.LabelUsers.Contains(labelUser))
            {
                return;
            }

            _context.LabelUsers.Remove(labelUser);
            _context.SaveChanges();
        }

        internal List<Label> AllFollowed(string userId)
        {
            return All().Where(l => l.Followers.Any(f => f.UserId == userId)).ToList();
        }
    }
}
