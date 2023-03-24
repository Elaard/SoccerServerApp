using Common.Helpers.HtmlConverter;
using EngineeringThesis.Api.Helpers.Abstract;
using Interfaces.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringThesis.Api.Helpers
{
    public class MainDataHelper:IMainDataHelper
    {
        private readonly ITeamFcd _teamFcd;
        private readonly IMatchFcd _matchFcd;
        private readonly ILeagueFcd _leagueFcd;
        private readonly ITeamMetaHelper _tm;

        private MainDataConverter _converter;

        public MainDataHelper(ILeagueFcd leagueFcd, ITeamFcd teamFcd, IMatchFcd matchFcd, ITeamMetaHelper tm)
        {
            _teamFcd = teamFcd;
            _matchFcd = matchFcd;
            _leagueFcd = leagueFcd;
            _tm = tm;

            _converter = new MainDataConverter();
        }
        public async Task AddLeague(string url)
        {
            await _converter.HandleHtmlAsync(url);

            var teamsStatistics = _converter.GetTeamStatistics();
            teamsStatistics.MetaTeams= await _tm.GetMetaTeamData(url);
            var matches = _converter.GetMatchSchedule();
            var league = _converter.GetLeague();

            league.UrlForHtml = url;
            int leagueId = _leagueFcd.AddLeague(league);

            await _teamFcd.AddTeams(teamsStatistics, leagueId);

            //tutaj mozna by podac ilosc druzyn
            _matchFcd.AddMatches(matches, leagueId);
        }
        public void RemoveLeague(int leagueId)
        {
            _matchFcd.RemoveMatchesByLeagueId(leagueId);
            _teamFcd.RemoveTeamsByLeagueId(leagueId);
            _leagueFcd.RemoveLeague(leagueId);
        }
       
        public async Task UpdateLeague(string url, int leagueId)
        {
            await _converter.HandleHtmlAsync(url);

            var teamsStatistics = _converter.GetTeamStatistics();
            var matches = _converter.GetMatchSchedule();

            _teamFcd.UpdateTeams(teamsStatistics, leagueId);
            _matchFcd.UpdateMatches(matches, leagueId);
        }
        public async Task UpdateLeagues()
        {
            var leagues = _leagueFcd.GetMappedLeagues();

            foreach (var league in leagues)
            {
                await UpdateLeague(league.UrlForHtml, league.Id);
            }
        }
       
    }
}
