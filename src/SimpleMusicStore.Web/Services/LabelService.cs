using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Data;
using SimpleMusicStore.Data.Models;
using SimpleMusicStore.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Services
{
    internal class LabelService : Service
    {
        internal LabelService(SimpleMusicStoreContext context)
            : base(context)
        {
        }

       


        internal async Task<List<Label>> AllAsync(string orderBy, string userId = null)
        {
            var labels = await AllAsync();
            if (orderBy == "alphabetically")
            {
                labels = labels.OrderBy(a => a.Name).ToList();
            }
            else if (orderBy == "popularity" || (orderBy == "recommended" && userId == null))
            {
                labels = labels.OrderByDescending(l => l.Followers.Count() + l.Records.Sum(r=>(r.Orders.Count() * 2) + r.WantedBy.Count())).ToList();
            }
            else if (orderBy == "recommended")
            {
                labels = labels.OrderByDescending(l=>
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
                labels = labels.ToList();
            }

            return labels;
        }



        private async Task<List<Label>> AllAsync()
        {
            return await _context.Labels
                .Include(l => l.Followers)
                .Include(l => l.Records)
                    .ThenInclude(r => r.Orders)
                        .ThenInclude(o => o.Order)
                .ToListAsync();
        }



        internal async Task<Label> GetAsync(int labelId)
        {
            return await _context.Labels
                .Include(l=>l.Records)
                    .ThenInclude(r => r.Artist)
                .Include(l=>l.Comments)
                    .ThenInclude(c=>c.User)
                .Include(l=>l.Followers)
                .FirstOrDefaultAsync(a => a.Id == labelId);
        }



        internal async Task<bool> IsValidIdAsync(int labelId)
        {
            return await _context.Labels.AnyAsync(l => l.Id == labelId);
        }



        internal async Task FollowAsync(int labelId, string userId)
        {
            if (!await IsValidIdAsync(labelId))
            {
                return;
            }
            if (await _context.LabelUsers.AnyAsync(lu => lu.LabelId == labelId && lu.UserId == userId))
            {
                return;
            }

            var labelUser = new LabelUser { LabelId = labelId, UserId = userId };

            await _context.LabelUsers.AddAsync(labelUser);
            await _context.SaveChangesAsync();
        }



        internal async Task UnfollowAsync(int labelId, string userId)
        {
            if (!await IsValidIdAsync(labelId))
            {
                return;
            }

            var labelUser = await _context.LabelUsers.FirstOrDefaultAsync(lu => lu.LabelId == labelId && lu.UserId == userId);

            if (labelUser == null)
            {
                return;
            }

            _context.LabelUsers.Remove(labelUser);
            await _context.SaveChangesAsync();
        }
    }
}
