using AutoMapper;
using Common.Helpers;
using Common.ModelsDTO.HtmlTables;
using Infrastructure.EFCore.Models;
using System.Text.RegularExpressions;

namespace Infrastructure.EFCore.AutoMapper
{
    public class LeagueProfile : Profile
    {
        public LeagueProfile()
        {
            CreateMap<LeagueDTO, League>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.Title, y => y.MapFrom(s => s.Title))
                .ForMember(x => x.UrlForHtml, y => y.MapFrom(s => s.UrlForHtml));

            CreateMap<League, LeagueDTO>()
                .ForMember(x => x.Id, y => y.MapFrom(s => s.Id))
                .ForMember(x => x.Title, y => y.MapFrom(s => StringMappingHelper.RemoveYears(s.Title)))
                .ForMember(x => x.UrlForHtml, y => y.MapFrom(s => s.UrlForHtml));
        }
    }
}
