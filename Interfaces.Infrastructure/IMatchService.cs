using Common.ModelsDTO;
using Common.ModelsDTO.HtmlTables;
using System.Collections.Generic;

namespace Interfaces.Infrastructure
{
    public interface IMatchService
    {
        void AddMatches(List<MatchDTO> model, int leagueId);
        void UpdateMatches(List<MatchDTO> matches, int leagueId);
        void RemoveMatchesByLeagueId(int leagueId);
        QueueDTO GetPreviousQueue(int leagueId, int queue);
        QueueDTO GetActualQueue(int leagueId);
        QueueDTO GetNextQueue(int leagueId, int queue);
        bool AnyMatchesExists(int leagueId);
        List<MatchQueueDTO> GetMatchesFromToday(int leagueId);
        List<TeamStatsDTO> GetTable(int leagueId);
    }
}
