using AutoMapper;
using SimpleMusicStore.Data.Models;
using SimpleMusicStore.Web.Areas.Admin.Models;
using SimpleMusicStore.Web.Areas.Admin.Models.DiscogsDtos;
using SimpleMusicStore.Web.Areas.Admin.Models.DiscogsDtos.RecordDtos;
using SimpleMusicStore.Web.Models.BindingModels;
using SimpleMusicStore.Web.Models.Dtos;
using SimpleMusicStore.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMusicStore
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            //discogs mappings
            CreateMap<DiscogsRecordVideoDto, Video>()
                .ForMember(v => v.Url, map => map.MapFrom(dto => dto.Uri));

            CreateMap<DiscogsLabelDto, Label>()
                .ForMember(l => l.ImageUrl, map => map.MapFrom(dto => dto.Images.First().Uri))
                .ForMember(l => l.DiscogsId, map => map.MapFrom(dto => dto.Id))
                .ForMember(l => l.Id, map => map.Ignore());

            CreateMap<DiscogsArtistDto, Artist>()
                .ForMember(a => a.ImageUrl, map => map.MapFrom(dto => dto.Images.First().Uri))
                .ForMember(a => a.DiscogsId, map => map.MapFrom(dto => dto.Id))
                .ForMember(a => a.Id, map => map.Ignore());

            CreateMap<DiscogsRecordTrackDto, Track>();
            
            CreateMap<DiscogsRecordDto, RecordAdminViewModel>()
                .ForMember(r => r.Artist, map => map.MapFrom(dto => dto.Artists.First().Name))
                .ForMember(r => r.Label, map => map.MapFrom(dto => dto.Labels.First().Name))
                .ForMember(r => r.Genre, map => map.MapFrom(dto => dto.Genres.First()))
                .ForMember(r => r.ImageUrl, map => map.MapFrom(dto => dto.Images.First().Uri))
                .ForMember(r => r.Format, map => map.MapFrom(dto => dto.Formats.First().Name));


            //view models
            CreateMap<Video, VideoDto>();

            CreateMap<Track, TrackDto>();

            CreateMap<Label, LabelDto>();

            CreateMap<Artist, ArtistDto>();

            CreateMap<Record, RecordViewModel>();

            

            CreateMap<Record, RecordAdminViewModel>()
                .ForMember(r => r.Artist, map => map.MapFrom(r => r.Artist.Name))
                .ForMember(r => r.Label, map => map.MapFrom(r => r.Label.Name));

            CreateMap<Record, RecordDto>();

            CreateMap<Label, LabelViewModel>();

            CreateMap<Artist, ArtistViewModel>();

            CreateMap<Comment, CommentDto>()
                .ForMember(c => c.User, map => map.MapFrom(c => c.User.UserName));



            CreateMap<AddressDto, Address>();

            CreateMap<Address, AddressDto>();

            CreateMap<RegisterBindingModel, SimpleUser>()
                .ForMember(u => u.UserName, map => map.MapFrom(rbm => rbm.Email));



            CreateMap<CartItemDto, RecordOrder>();


            CreateMap<RecordOrder, CartRecordViewModel>()
                .ForMember(cr => cr.Artist, map => map.MapFrom(ro => ro.Record.Artist))
                .ForMember(cr => cr.Label, map => map.MapFrom(ro => ro.Record.Label))
                .ForMember(cr => cr.Price, map => map.MapFrom(ro => ro.Record.Price))
                .ForMember(cr => cr.Title, map => map.MapFrom(ro => ro.Record.Title));

            CreateMap<CartOrderViewModel, Order>();

            CreateMap<Order, CartOrderViewModel>();

            CreateMap<Record, CartRecordViewModel>();

            CreateMap<RecordUser, RecordDto>()
                .ForMember(dto=>dto.Id, map=>map.MapFrom(ru=>ru.RecordId))
                .ForMember(dto => dto.Artist, map => map.MapFrom(ru => ru.Record.Artist))
                .ForMember(dto => dto.Label, map => map.MapFrom(ru => ru.Record.Label))
                .ForMember(dto => dto.Title, map => map.MapFrom(ru => ru.Record.Title));

            CreateMap<ArtistUser, ArtistDto>()
                .ForMember(dto => dto.Id, map => map.MapFrom(au => au.ArtistId))
                .ForMember(dto => dto.Name, map => map.MapFrom(au => au.Artist.Name));

            CreateMap<LabelUser, LabelDto>()
                .ForMember(dto => dto.Id, map => map.MapFrom(lu => lu.LabelId))
                .ForMember(dto => dto.Name, map => map.MapFrom(lu => lu.Label.Name));
        }
    }
}