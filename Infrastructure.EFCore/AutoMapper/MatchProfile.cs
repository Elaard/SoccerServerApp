using AutoMapper;
using Infrastructure.EFCore.Models;

namespace Infrastructure.EFCore.AutoMapper
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<Match, Match>()
                .ForMember(x => x.Id, y => y.Ignore());
        }
    }

}
