using Common.ModelsDTO.HtmlTables;
using Interfaces.Facades;
using Interfaces.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Facades
{
    public class TeamFcd : ITeamFcd
    {
        private readonly ITeamService _teamSrv;
        private readonly ILeagueService _leagueService;

        public TeamFcd(ITeamService teamSrv, ILeagueService leagueService)
        {
            _teamSrv = teamSrv;
            _leagueService = leagueService;
        }

        public async Task AddTeams(TeamDTO teams, int leagueId)
        {
            try
            {
                await _teamSrv.AddTeams(teams, leagueId);
            }
            
            catch (Exception ex)
            {
                _leagueService.RemoveLeague(leagueId);
            }
        }

        public void RemoveTeamsByLeagueId(int leagueId)
        {
            _teamSrv.RemoveTeamsByLeagueId(leagueId);
        }

        public void UpdateTeams(TeamDTO teams, int leagueId)
        {
            _teamSrv.UpdateTeams(teams, leagueId);
        }
    }
}
