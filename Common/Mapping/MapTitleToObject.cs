using Common.ModelsDTO.HtmlTables;
using System.Reflection;

namespace Common.Mapping
{
    public static class MapTitleToObject
    {
        public static LeagueDTO Map(string title)
        {
            LeagueDTO league = new LeagueDTO();

            var splitedTitle = title.Split(" ");
            PropertyInfo[] leagueProps = typeof(LeagueDTO).GetProperties();

            for (int i = 0; i < splitedTitle.Length; i++)
            {
                leagueProps[i].SetValue(league, splitedTitle[i]);
            }
            return league;
        }
    }
}
