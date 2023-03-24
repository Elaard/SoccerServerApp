using Common.Helpers;
using Common.ModelsDTO.HtmlTables;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Mapping
{
    public static class MapMetaTeamToObject
    {
        public static MetaTeamDTO Map(List<List<string>> table)
        {
            List<MetaTeamDTO> teams = new List<MetaTeamDTO>();
            PropertyInfo[] teamProps = typeof(MetaTeamDTO).GetProperties();

            MetaTeamDTO team = new MetaTeamDTO();

            for (int p = 0; p < teamProps.Length; p++)
            {
                teamProps[p].SetValue(team, table[p][0]);
            }
            return team;
        }
            
    }
}
