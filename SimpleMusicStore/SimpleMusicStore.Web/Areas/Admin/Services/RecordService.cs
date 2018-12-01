using AutoMapper;
using Newtonsoft.Json;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Areas.Admin.Models;
using SimpleMusicStore.Web.Areas.Admin.Models.RecordDtos;
using SimpleMusicStore.Web.Areas.Admin.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Areas.Admin.Services
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
       
        internal void AddRecord(RecordDto recordDto, decimal price)
        {
            var record = GetRecord(recordDto);

            if(record is null)
            {
                return;
            }
            record.Price = price;

            _context.Records.Add(record);
            _context.SaveChanges();
        }

        private Record GetRecord(RecordDto recordDto)
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
                var labelDto = DiscogsUtilities.Get<LabelDto>(labelInfo.Id);
                label = _mapper.Map<Label>(labelDto);
            }

            var artistInfo = recordDto.Artists.First();
            var artist = _context.Artists.FirstOrDefault(a => a.DiscogsId == artistInfo.Id);
            if (artist is null)
            {
                var artistDto = DiscogsUtilities.Get<ArtistDto>(artistInfo.Id);
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
    }
}
