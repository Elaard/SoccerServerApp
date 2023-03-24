using Common.ModelsDTO.HtmlTables;
using System.Threading.Tasks;

namespace Interfaces.Facades
{
    public interface ITeamFcd
    {
        Task AddTeams(TeamDTO teams, int leagueId);
        void UpdateTeams(TeamDTO teams, int leagueId);
        void RemoveTeamsByLeagueId(int leagueId);
    }
}
