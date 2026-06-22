using AutoMapper;
using SoccerProjectCase5.WebApi.Dtos.FixtureDtos;
using SoccerProjectCase5.WebApi.Dtos.MatchEventDtos;
using SoccerProjectCase5.WebApi.Dtos.MatchStatisticDtos;
using SoccerProjectCase5.WebApi.Dtos.TeamDtos;
using SoccerProjectCase5.WebApi.Entities;

namespace SoccerProjectCase5.WebApi.Mapping
{
    public class GeneralMapping : Profile 
    {
        public GeneralMapping()
        {
            // Team Mapping
            CreateMap<Team, CreateTeamDto>().ReverseMap();

            // Fixture Mapping
            CreateMap<Fixture, CreateFixtureDto>().ReverseMap();
            CreateMap<Fixture, UpdateFixtureDto>().ReverseMap();

            // Tekli getirme işleminde ilişkili tablolardan (Team) veri çekmek için
            CreateMap<Fixture, GetFixtureByIdDto>()
                .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.TeamName))
                .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam.TeamName))
                .ForMember(dest => dest.HomeLogoUrl, opt => opt.MapFrom(src => src.HomeTeam.LogoUrl))
                .ForMember(dest => dest.AwayLogoUrl, opt => opt.MapFrom(src => src.AwayTeam.LogoUrl));

            // ÇOK ÖNEMLİ EKLEME: Tüm listeyi dönerken de takım isimlerine ve logolara ihtiyacımız var!
            CreateMap<Fixture, ResultFixtureDto>()
                .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.TeamName))
                .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam.TeamName))
                .ForMember(dest => dest.HomeLogoUrl, opt => opt.MapFrom(src => src.HomeTeam.LogoUrl))
                .ForMember(dest => dest.AwayLogoUrl, opt => opt.MapFrom(src => src.AwayTeam.LogoUrl));

            // MatchEvent Mapping
            CreateMap<MatchEvent, CreateMatchEventDto>().ReverseMap();
            CreateMap<MatchEvent, UpdateMatchEventDto>().ReverseMap();
            CreateMap<MatchEvent, MatchEventDto>().ReverseMap();

            // MatchStatistic Mapping 
            CreateMap<MatchStatistic, CreateMatchStatisticDto>().ReverseMap();
            CreateMap<MatchStatistic, UpdateMatchStatisticDto>().ReverseMap();
            CreateMap<MatchStatistic, MatchStatisticDto>().ReverseMap();

        }
    }
}
