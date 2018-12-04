using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Areas.Admin.Models.DiscogsDtos;
using SimpleMusicStore.Web.Areas.Admin.Models.DiscogsDtos.RecordDtos;
using SimpleMusicStore.Web.Areas.Admin.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Services
{
    internal class RecordService
    {
        
        private SimpleDbContext _context;
        private IMapper _mapper;
        private string _userId;

        public RecordService(SimpleDbContext context)
        {
            _context = context;
        }

        public RecordService(SimpleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public RecordService(SimpleDbContext context,  string userId)
        {
            _context = context;
            _userId = userId;
        }


        public RecordService(SimpleDbContext context, IMapper mapper, string userId)
        {
            _context = context;
            _mapper = mapper;
            _userId = userId;
        }




        internal void AddRecord(DiscogsRecordDto discogsRecordDto, decimal price)
        {
            var record = CreateRecord(discogsRecordDto);

            if(record is null)
            {
                return;
            }
            record.Price = price;

            _context.Records.Add(record);
            _context.SaveChanges();
        }




        private Record CreateRecord(DiscogsRecordDto recordDto)
        {
            if (_context.Records.Any(r => r.DiscogsId == recordDto.Id))
            {
                return null;
            }


            var videos = recordDto.Videos.Select(_mapper.Map<Video>).ToList();

            var labelInfo = recordDto.Labels.First();
            var label = _context.Labels.FirstOrDefault(l => l.DiscogsId == labelInfo.Id);
            if (label is null)
            {
                var labelDto = DiscogsUtilities.Get<DiscogsLabelDto>(labelInfo.Id);
                label = _mapper.Map<Label>(labelDto);
            }

            var artistInfo = recordDto.Artists.First();
            var artist = _context.Artists.FirstOrDefault(a => a.DiscogsId == artistInfo.Id);
            if (artist is null)
            {
                var artistDto = DiscogsUtilities.Get<DiscogsArtistDto>(artistInfo.Id);
                artist = _mapper.Map<Artist>(artistDto);
            }

            var discogsId = recordDto.Id;

            var genre = recordDto.Genres.First();

            var year = recordDto.Year;

            var tracks = recordDto.Tracklist.Select(_mapper.Map<Track>).ToList();

            var imageUrl = recordDto.Images.First().Uri;

            var title = recordDto.Title;

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
                Title = title
            };

            return record;
        }




        internal int FindByDiscogsId(long discogsRecordId)
        {
            var record = _context.Records.FirstOrDefault(r => r.DiscogsId == discogsRecordId);

            if (record is null)
            {
                return -1;
            }

            return record.Id;
        }




        internal bool IsValidRecordId(int recordId)
        {
            return _context.Records.Any(r => r.Id == recordId);
        }




        internal Record GetRecord(int recordId)
        {
            return _context.Records
                .Include(r=>r.Artist)
                .Include(r=>r.Label)
                .Include(r=>r.Videos)
                .Include(r=>r.Tracks)
                .Include(r=>r.Comments)
                    .ThenInclude(c=>c.User.UserName)
                .FirstOrDefault(r => r.Id == recordId);
        }




        internal void EditRecordPrice(int recordId, decimal price)
        {
            var record = GetRecord(recordId);
            if (record.Price == price)
            {
                return;
            }

            record.Price = price;
            _context.SaveChanges();
        }




        internal void RemoveRecord(int recordId)
        {
            var record = GetRecord(recordId);

            if (record is null)
            {
                return;
            }

            _context.Records.Remove(record);
            _context.SaveChanges();
        }




        internal List<Record> All(string orderBy, List<string> genres = null)
        {
            List<Record> records;

            if (orderBy == "newest")
            {
                records = All(genres).OrderByDescending(r => r.DateAdded).ToList();
            }
            else if (orderBy == "alphabetically")
            {
                records = All(genres).OrderBy(r => r.Title).ToList();
            }
            else if (orderBy == "popularity" || (orderBy == "recommended" && _userId == ""))
            {
                records = All(genres).OrderByDescending(r => r.WantedBy.Count() + (r.Orders.Count() * 2)).ToList();
            }
            else if (orderBy == "recommended")
            {
                records = All(genres).OrderByDescending(r =>
                {
                    if (r.Orders.Any(o => o.Order.UserId == _userId))
                    {
                        return -1;
                    }
                    
                    var artistIsFollowed = r.Artist.Followers.Where(f => f.UserId == _userId).Count() + 5;
                    var labelIsFollowed = r.Label.Followers.Where(f => f.UserId == _userId).Count() + 5;
                    var artistOrLabelOrderCount = r.Orders.Where(o => o.Record.ArtistId == r.ArtistId || o.Record.LabelId == r.LabelId).Count();

                    return artistIsFollowed + labelIsFollowed + artistOrLabelOrderCount;
                })
                .ToList();
            }
            else
            {
                records = All().ToList();
            }

            return records;
        }




        private List<Record> All(List<string> genres = null)
        {
            var records = _context.Records
                .Include(r => r.Artist)
                    .ThenInclude(a => a.Followers)
                .Include(r => r.Label)
                    .ThenInclude(l => l.Followers)
                .Include(r => r.WantedBy)
                .Include(r => r.Orders)
                    .ThenInclude(o => o.Order)
                .ToList();

            if (genres != null)
            {
                records = records.Where(r => genres.Contains(r.Genre)).ToList();
            }
            return records;
        }




        internal void AddToWantlist(int recordId)
        {
            if (!IsValidRecordId(recordId))
            {
                return;
            }

            var recordUser = new RecordUser { RecordId = recordId, UserId = _userId };

            if (_context.RecordUsers.Contains(recordUser))
            {
                return;
            }
            _context.RecordUsers.Add(recordUser);
            _context.SaveChanges();
        }

        internal void RemoveFromWantlist(int recordId)
        {
            if (!IsValidRecordId(recordId))
            {
                return;
            }

            var recordUser = new RecordUser { RecordId = recordId, UserId = _userId };

            if (!_context.RecordUsers.Contains(recordUser))
            {
                return;
            }
            _context.RecordUsers.Remove(recordUser);
            _context.SaveChanges();

        }





    }
}
