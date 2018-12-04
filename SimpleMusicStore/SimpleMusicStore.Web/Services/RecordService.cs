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
        
        public RecordService(SimpleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        internal bool IsValidRecordId(int id)
        {
            return _context.Records.Any(r => r.Id == id);
        }

        internal Record GetRecord(int id)
        {
            return _context.Records
                .Include(r=>r.Artist)
                .Include(r=>r.Label)
                .Include(r=>r.Videos)
                .Include(r=>r.Tracks)
                .FirstOrDefault(r => r.Id == id);
        }

        internal void EditRecordPrice(int id, decimal price)
        {
            var record = GetRecord(id);
            record.Price = price;
            _context.SaveChanges();
        }

        internal void RemoveRecord(int id)
        {
            var record = GetRecord(id);

            _context.Records.Remove(record);
            _context.SaveChanges();
        }

        internal List<Record> All()
        {
            return _context.Records
                .Include(r => r.Artist)
                .Include(r => r.Label)
                .Include(r => r.Videos)
                .Include(r => r.Tracks)
                .Include(r => r.WantedBy)
                .Include(r => r.Orders)
                .ToList();
        }

        internal List<Record> All(string genre)
        {
            return _context.Records
                .Where(r=>r.Genre == genre)
                .Include(r => r.Artist)
                .Include(r => r.Label)
                .Include(r => r.Videos)
                .Include(r => r.Tracks)
                .Include(r => r.WantedBy)
                .Include(r => r.Orders)
                .ToList();
        }

        internal bool IsValidGenre(string genre)
        {
            return _context.Records.Any(r => r.Genre == genre);
        }
    }
}
