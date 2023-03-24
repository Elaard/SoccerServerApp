using System.Collections.Generic;

namespace Common.ModelsDTO.HtmlTables
{
    public class TeamDTO
    {
        public List<TeamStatisticsDTO> Stats { get; set; }
        public List<MetaTeamDTO> MetaTeams { get; set; }
    }
}
