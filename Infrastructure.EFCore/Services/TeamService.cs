using AutoMapper;
using Common.CustomNaming;
using Common.Helpers;
using Common.ModelsDTO.HtmlTables;
using Infrastructure.EFCore.Models;
using Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.EFCore.Services
{
    public class TeamService : ITeamService
    {
        private readonly Context _context;
        private readonly ILogger<TeamService> _logger;
        private readonly IMapper _mapper;

        public TeamService(Context context, ILogger<TeamService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task AddTeams(TeamDTO teams, int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            List<Team> teamsToAdd = MapTeams(teams.Stats);

            foreach (var team in teamsToAdd)
            {
                team.LeagueId = leagueId;
            }
            for (int i = 0; i < teams.MetaTeams.Count; i++)
            {
                MetaTeam mt = new MetaTeam
                {
                    Url = teams.MetaTeams[i].Url
                };
                teamsToAdd[i].MetaTeam = mt;
                teamsToAdd[i].YearOfFundation = StringMappingHelper.GetStringAfterSecondSpace(teams.MetaTeams[i].YearOfFundation);
                teamsToAdd[i].Colors = StringMappingHelper.GetStringAfterSpace(teams.MetaTeams[i].Colors);
                teamsToAdd[i].PageWWW = StringMappingHelper.GetStringAfterSpace(teams.MetaTeams[i].PageWWW);
                teamsToAdd[i].Address = StringMappingHelper.GetStringsAfterSpace(teams.MetaTeams[i].Address);
                teamsToAdd[i].Photo = StringMappingHelper.GetStringBetweenQuatationonMark(teams.MetaTeams[i].Photo);
            }
            await _context.AddRangeAsync(teamsToAdd);
            await _context.SaveChangesAsync();

        }
        public void UpdateTeams(TeamDTO teams, int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            var teamsToUpdate = GetTeamsByLeagueId(leagueId);
            var updatingTeams = MapTeams(teams.Stats);

            foreach (var team in teamsToUpdate)
            {
                var tm = updatingTeams.FirstOrDefault(x => x.Name == team.Name);
            }
            _context.UpdateRange(teamsToUpdate);
            _context.SaveChanges();
        }
        public void RemoveTeamsByLeagueId(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            //var teams = GetTeamsByLeagueId(leagueId);

            var teams = _context.Teams.Where(x => x.LeagueId == leagueId).Include(d => d.HomeMatches).Include(y => y.AwayMatches).ToList();

            _context.RemoveRange(teams);
            var g = _context.SaveChanges();
        }
        private List<Team> MapTeams(List<TeamStatisticsDTO> model)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            List<Team> teams = new List<Team>();

            foreach (TeamStatisticsDTO item in model)
            {
                Team team = new Team
                {
                    Name = StringMappingHelper.GetStringAfterSemicolon(item.Name)
                };
                teams.Add(team);
            }
            return teams;
        }
        private List<Team> GetTeamsByLeagueId(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _context.Teams.Where(x => x.LeagueId == leagueId).ToList();
        }


    }
}
