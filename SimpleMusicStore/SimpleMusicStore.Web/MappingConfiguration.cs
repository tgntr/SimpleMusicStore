using AutoMapper;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Areas.Admin.Models;
using SimpleMusicStore.Web.Areas.Admin.Models.RecordDtos;
using System.Linq;

namespace SimpleMusicStore
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<RecordVideoDto, Video>()
                .ForMember(v => v.Url, map => map.MapFrom(dto => dto.Uri));

            CreateMap<LabelDto, Label>()
                .ForMember(l=>l.ImageUrl, map=>map.MapFrom(dto=>dto.Images.First().Uri))
                .ForMember(l => l.DiscogsId, map => map.MapFrom(dto => dto.Id))
                .ForMember(l => l.Id, map => map.Ignore());

            CreateMap<ArtistDto, Artist>()
                .ForMember(a => a.ImageUrl, map => map.MapFrom(dto => dto.Images.First().Uri))
                .ForMember(a => a.DiscogsId, map => map.MapFrom(dto => dto.Id))
                .ForMember(a => a.Id, map => map.Ignore());

            CreateMap<RecordTrackDto, Track>();

           
        }
    }
}