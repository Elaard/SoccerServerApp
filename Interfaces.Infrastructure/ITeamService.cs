using Common.ModelsDTO.HtmlTables;
using System.Threading.Tasks;

namespace Interfaces.Infrastructure
{
    public interface ITeamService
    {
        Task AddTeams(TeamDTO teams, int leagueId);
        void UpdateTeams(TeamDTO teams, int leagueId);
        void RemoveTeamsByLeagueId(int leagueId);
    }
}
