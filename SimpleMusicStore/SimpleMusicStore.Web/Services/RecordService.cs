using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Areas.Admin.Models.DiscogsDtos;
using SimpleMusicStore.Web.Areas.Admin.Models.DiscogsDtos.RecordDtos;
using SimpleMusicStore.Web.Areas.Admin.Utilities;
using SimpleMusicStore.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Services
{
    internal class RecordService : Service
    {
        private IMapper _mapper;

        
        public RecordService(SimpleDbContext context, IMapper mapper)
            :base (context)
        {
            _mapper = mapper;
        }


        public RecordService(SimpleDbContext context)
            : base(context)
        {
        }




        internal async Task AddRecord(DiscogsRecordDto discogsRecordDto, decimal price)
        {
            var record = await CreateRecord(discogsRecordDto);

            if(record is null)
            {
                return;
            }
            record.Price = price;

            await _context.Records.AddAsync(record);
            await _context.SaveChangesAsync();
        }




        private async Task<Record> CreateRecord(DiscogsRecordDto recordDto)
        {
            if (await _context.Records.AnyAsync(r => r.DiscogsId == recordDto.Id))
            {
                return null;
            }
            List<Video> videos = new List<Video>();
            if (recordDto.Videos != null)
            {
                videos = recordDto.Videos.Select(_mapper.Map<Video>).ToList();
            }

            var labelInfo = recordDto.Labels.First();
            var label = await _context.Labels.FirstOrDefaultAsync(l => l.DiscogsId == labelInfo.Id);
            if (label is null)
            {
                var labelDto = await DiscogsUtilities.GetAsync<DiscogsLabelDto>(labelInfo.Id);
                label = _mapper.Map<Label>(labelDto);
            }

            var artistInfo = recordDto.Artists.First();
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.DiscogsId == artistInfo.Id);
            if (artist is null)
            {
                var artistDto = await DiscogsUtilities.GetAsync<DiscogsArtistDto>(artistInfo.Id);
                artist = _mapper.Map<Artist>(artistDto);
            }

            var discogsId = recordDto.Id;

            var genre = recordDto.Genres.First();

            var year = recordDto.Year;

            var tracks = recordDto.Tracklist.Select(_mapper.Map<Track>).ToList();

            var imageUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/12in-Vinyl-LP-Record-Angle.jpg/330px-12in-Vinyl-LP-Record-Angle.jpg";
            if (recordDto.Images != null)
            {
                imageUrl = recordDto.Images.First().Uri;
            }

            var title = recordDto.Title;

            var format = recordDto.Formats.First().Name;

            var record = new Record
            {
                Videos = videos,
                Label = label,
                Artist = artist,
                DiscogsId = discogsId,
                Genre = genre,
                Year = year,
                Tracks = tracks,
                ImageUrl = imageUrl,
                Title = title,
                Format = format
            };

            return record;
        }




        internal async Task<int> FindByDiscogsId(long discogsRecordId)
        {
            var record = await _context.Records.FirstOrDefaultAsync(r => r.DiscogsId == discogsRecordId);

            if (record is null)
            {
                return -1;
            }

            return record.Id;
        }




        internal async Task<bool> IsValidRecordIdAsync(int recordId)
        {
            return await _context.Records.AnyAsync(r => r.Id == recordId);
        }

        internal bool IsValidRecordId(int recordId)
        {
            return  _context.Records.Any(r => r.Id == recordId);
        }


        internal async Task<Record> GetRecordAsync(int recordId)
        {
            return await _context.Records
                .Include(r=>r.Artist)
                .Include(r=>r.Label)
                .Include(r=>r.Videos)
                .Include(r=>r.Tracks)
                .Include(r=>r.Comments)
                    .ThenInclude(c=>c.User)
                .Include(r=>r.WantedBy)
                .FirstOrDefaultAsync(r => r.Id == recordId);
        }

        internal Record GetRecord(int recordId)
        {
            return  _context.Records
                .Include(r => r.Artist)
                .Include(r => r.Label)
                .Include(r => r.Videos)
                .Include(r => r.Tracks)
                .Include(r => r.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefault(r => r.Id == recordId);
        }


        internal async Task EditRecordPrice(int recordId, decimal price)
        {
            var record = await GetRecordAsync(recordId);
            if (record.Price == price)
            {
                return;
            }

            record.Price = price;
            await _context.SaveChangesAsync();
        }




        internal async Task RemoveRecord(int recordId)
        {
            var record = await GetRecordAsync(recordId);

            if (record is null)
            {
                return;
            }

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
        }




        internal async Task<List<Record>> All(string orderBy, string userId = null, List<string> genres = null)
        {
            var records = await All();

            if (orderBy == "newest")
            {
                records = records.OrderByDescending(r => r.DateAdded).ToList();
            }
            else if (orderBy == "alphabetically")
            {
                records = records.OrderBy(r => r.Title).ToList();
            }
            else if (orderBy == "popularity" || (orderBy == "recommended" && userId == null))
            {
                records = records.OrderByDescending(r => r.WantedBy.Count() + (r.Orders.Count() * 2)).ToList();
            }
            else if (orderBy == "recommended")
            {
                records = records.OrderByDescending(record =>
                {
                    if (record.Orders.Any(o => o.Order.UserId == userId))
                    {
                        return -1;
                    }

                    var artistIsFollowed = 0;
                    if (record.Artist.Followers.Any(f => f.UserId == userId))
                    {
                        artistIsFollowed = 10;
                    }
                    var labelIsFollowed = 0;
                    if (record.Label.Followers.Any(f => f.UserId == userId))
                    {
                        labelIsFollowed = 10;
                    }
                    var artistOrLabelOrderCount = record.Orders.Where(o => (o.Record.ArtistId == record.ArtistId || o.Record.LabelId == record.LabelId) && o.Order.UserId == userId).Count();
                    var artistOrLabelWantlistCount = record.WantedBy.Where(w => (w.Record.ArtistId == record.ArtistId || w.Record.LabelId == record.LabelId) && w.UserId == userId).Count();


                    return artistIsFollowed + labelIsFollowed + artistOrLabelOrderCount;
                })
                .ToList();
            }
            else
            {
                records = records.ToList();
            }

            return records;
        }




        private async Task<List<Record>> All(List<string> genres = null)
        {
            var records = await _context.Records
                .Include(r => r.Artist)
                    .ThenInclude(a => a.Followers)
                .Include(r => r.Label)
                    .ThenInclude(l => l.Followers)
                .Include(r => r.WantedBy)
                .Include(r => r.Orders)
                    .ThenInclude(o => o.Order)
                .ToListAsync();

            if (genres != null && genres.Count > 0)
            {
                records = records.Where(r => genres.Contains(r.Genre)).ToList();
            }
            return records;
        }




        internal async Task AddToWantlist(int recordId, string userId)
        {
            if (!await IsValidRecordIdAsync(recordId))
            {
                return;
            }
            if (await _context.RecordUsers.AnyAsync(ru=>ru.RecordId == recordId && ru.UserId == userId))
            {
                return;
            }

            var recordUser = new RecordUser { RecordId = recordId, UserId = userId };

            
            await _context.RecordUsers.AddAsync(recordUser);
            await _context.SaveChangesAsync();
        }



        internal async Task RemoveFromWantlist(int recordId, string userId)
        {
            if (!await IsValidRecordIdAsync(recordId))
            {
                return;
            }

            var recordUser = await _context.RecordUsers.FirstOrDefaultAsync(ru => ru.RecordId == recordId && ru.UserId == userId);

            if (recordUser == null)
            {
                return;
            }

            _context.RecordUsers.Remove(recordUser);
            await _context.SaveChangesAsync();

        }



    }
}
