using Common.ModelsDTO.HtmlTables;
using System.Collections.Generic;
using System.Reflection;

namespace Common.Mapping
{
    public static class MapTableToObject
    {
        public static List<TeamStatisticsDTO> MapFromTableMainClass(List<List<string>> table)
        {
            List<TeamStatisticsDTO> teams = new List<TeamStatisticsDTO>();
            PropertyInfo[] teamProps = typeof(TeamStatisticsDTO).GetProperties();

            for (int i = 0; i < table.Count; i++)
            {
                TeamStatisticsDTO team = new TeamStatisticsDTO();
                for (int p = 0; p < teamProps.Length; p++)
                {
                    var tr = table[i];
                    teamProps[p].SetValue(team, tr[p]);
                }
                teams.Add(team);
            }
            return teams;
        }
        public static List<MatchDTO> MapFromTableMainClass2(List<List<string>> table)
        {
            List<MatchDTO> teams = new List<MatchDTO>();
            PropertyInfo[] teamProps = typeof(MatchDTO).GetProperties();

            for (int i = 0; i < table.Count; i++)
            {
                MatchDTO team = new MatchDTO();
                for (int p = 0; p < teamProps.Length; p++)
                {
                    var tr = table[i];
                    teamProps[p].SetValue(team, tr[p]);
                }
                teams.Add(team);
            }
            return teams;
        }
    }
}
