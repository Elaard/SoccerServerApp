using Common.ModelsDTO.HtmlTables;
using System.Collections.Generic;

namespace Interfaces.Infrastructure
{
    public interface ILeagueService
    {
        int AddLeague(LeagueDTO league);
        LeagueDTO GetMappedLeagueByHtml(string html);
        LeagueDTO GetMappedLeagueById(int urlId);
        List<LeagueDTO> GetMappedLeagues();
        int RemoveLeague(int id);
        bool LeagueExists(int id);
        bool LeagueExists(string title);
        bool IsAnyLeagueExists();
    }
}
