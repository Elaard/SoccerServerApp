using AutoMapper;
using Common.Helpers;
using Common.ModelsDTO.HtmlTables;
using Infrastructure.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EFCore.AutoMapper
{
    public class TeamProfile:Profile
    {
        public TeamProfile()
        {
            CreateMap<MetaTeamDTO, MetaTeam>()
               .ForMember(x => x.Id, y => y.Ignore())
               .ForMember(x => x.Url, y => y.MapFrom(s => StringMappingHelper.GetStringAfterSpace(s.Url)));

            CreateMap<MetaTeamDTO, Team>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.Colors, y => y.MapFrom(s => StringMappingHelper.GetStringAfterSpace(s.Colors)))
                .ForMember(x => x.YearOfFundation, y => y.MapFrom(s => StringMappingHelper.GetStringAfterSpace(s.YearOfFundation)))
                .ForMember(x => x.PageWWW, y => y.MapFrom(s => StringMappingHelper.GetStringAfterSpace(s.PageWWW)));
                

        }
    }
}
