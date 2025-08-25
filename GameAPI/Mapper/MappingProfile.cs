using AutoMapper;
using GameAPI.DTOs;
using GameAPI.Models;

namespace GameAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<UserDto, User>();

            // GameCompany
            CreateMap<GameCompanyDto, GameCompany>();

            // Game
            CreateMap<GameDto, Game>();

            // GameDetail
            CreateMap<GameDetailDto, GameDetail>();

            // Platform
            CreateMap<PlatformDto, Platform>();

            // Publisher
            CreateMap<PublisherDto, Publisher>();

            // return DTOs to client instead of entities
            CreateMap<User, UserDto>();
            CreateMap<GameCompany, GameCompanyDto>();
            CreateMap<Game, GameDto>();
            CreateMap<GameDetail, GameDetailDto>();
            CreateMap<Platform, PlatformDto>();
            CreateMap<Publisher, PublisherDto>();

            CreateMap<Game, GameDto>().ReverseMap();

            CreateMap<GameDetail, GameDetailDto>().ReverseMap();

            CreateMap<GameCompany, GameCompanyDto>().ReverseMap();
        }
    }
}
