using Common.CustomNaming;
using Common.Extensions;
using Common.Helpers;
using Common.ModelsDTO;
using Common.ModelsDTO.HtmlTables;
using Infrastructure.EFCore.Models;
using Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.EFCore.Services
{
    public class MatchService : IMatchService
    {
        private readonly Context _context;
        private readonly ILogger<MatchService> _logger;

        public MatchService(Context context, ILogger<MatchService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void AddMatches(List<MatchDTO> model, int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            List<Match> matches = MapMatches(model, leagueId);

            _context.AddRange(matches);
            _context.SaveChanges();
        }
        public void UpdateMatches(List<MatchDTO> matches, int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            RemoveMatchesByLeagueId(leagueId);

            var updatingMatches = MapMatches(matches, leagueId);

            _context.AddRange(updatingMatches);
            _context.SaveChanges();
        }
        public void RemoveMatchesByLeagueId(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            var matches = GetMatchesByLeagueId(leagueId);

            _context.RemoveRange(matches);
            _context.SaveChanges();
        }

        public QueueDTO GetPreviousQueue(int leagueId, int queue)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            queue = queue > 1 ? queue - 1 : 1;

            var matches = GetMappedMatchesByLeagueAndQueue(leagueId, queue);

            var model = new QueueDTO
            {
                QueueNumber = queue,
                Queues = SetOrder(matches)
            };

            return model;
        }
        private List<Match> GetMatchesFromPreviousRound(IQueryable<Match> matches)
        {
            List<Match> prevRound = new List<Match>();
            foreach (var match in matches)
            {
                prevRound.Add(_context.Matches.Where(x => x.AwayTeamId == match.HomeTeamId && x.HomeTeamId == match.AwayTeamId).FirstOrDefault());
            }
            return prevRound;
        }
        public QueueDTO GetActualQueue(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            var queue = GetCurrentQueue(leagueId);

            if (queue == 0)
                queue = GetLastQueueNumber(leagueId, true);

            //get matches from league and current queue
            var matches = GetMappedMatchesByLeagueAndQueue(leagueId, queue);

            //if the entire queue is moved to another date
            while (matches.Count == 0 && queue != GetLastQueueNumber(leagueId))
            {
                matches = GetMappedMatchesByLeagueAndQueue(leagueId, queue + 1);
            }

            var model = new QueueDTO
            {
                QueueNumber = queue,
                Queues = SetOrder(matches)
            };

            return model;
        }

        public QueueDTO GetNextQueue(int leagueId, int queue)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            int lastQ = GetLastQueueNumber(leagueId);
            queue = queue > 0 && queue < lastQ ? queue + 1 : lastQ;

            var matches = GetMappedMatchesByLeagueAndQueue(leagueId, queue);

            var model = new QueueDTO
            {
                QueueNumber = queue,
                Queues = SetOrder(matches)
            };
            return model;
        }
        public List<MatchQueueDTO> GetMappedMatchesByLeagueAndQueue(int leagueId, int queue)
        {
            var matches = GetMatchesByLeagueAndQueue(leagueId, queue);

            return MapToMatchQueueDTO(matches);
        }
        private List<MatchQueueDTO> MapToMatchQueueDTO(IQueryable<Match> matches)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            var prevRound = GetMatchesFromPreviousRound(matches);

            var mappedMatches = matches.Select(m => new MatchQueueDTO
            {
                HomeTeamId = m.HomeTeamId,
                AwayTeamId = m.AwayTeamId,

                Date = m.Date.Value.ToString("dd.MM.yyyy"),
                Time = m.Date.Value.ToString("HH:mm"),

                HomeTeamName = m.HomeTeam.Name,
                AwayTeamName = m.AwayTeam.Name,

                HomeTeamGoals = m.HomeTeamGoals,
                AwayTeamGoals = m.AwayTeamGoals
            }).ToList();

            foreach (var match in mappedMatches)
            {
                var prev = prevRound.Where(x => x.AwayTeamId == match.HomeTeamId && x.HomeTeamId == match.AwayTeamId).FirstOrDefault();
                match.PrevMatch = new PrevMatchRoundDTO
                {
                    AwayTeamGoals = prev.HomeTeamGoals,
                    HomeTeamGoals = prev.AwayTeamGoals
                };
            }


            return mappedMatches;
        }
        private List<Match> MapMatches(List<MatchDTO> model, int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            List<Match> matches = new List<Match>();

            List<Team> teamsToMap = GetTeamsByLeagueId(leagueId);

            foreach (MatchDTO item in model)
            {
                int? homeTeamGoals = StringMappingHelper.GetNumberBeforeDash(item.Score);
                int? awayTeamGoals = StringMappingHelper.GetNumberAfterDash(item.Score);

                int homeTeamId = teamsToMap.Where(x => x.Name == item.HomeTeam).Select(y => y.Id).FirstOrDefault();
                int awayTeamId = teamsToMap.Where(x => x.Name == item.AwayTeam).Select(y => y.Id).FirstOrDefault();

                Dictionary<int, int?> teams = new Dictionary<int, int?>();
                teams.Add(homeTeamId, awayTeamGoals);
                teams.Add(awayTeamId, awayTeamGoals);

                int? winnerTeamId = awayTeamGoals == homeTeamGoals ? null : DictionaryValuesHelper.GetKeyBasedOnBiggestValueInDictContainsTwoPairs(teams);

                Match match = new Match
                {
                    Date = PullOutDateFromString.Get(item.Date),

                    HomeTeamId = homeTeamId,
                    HomeTeamGoals = homeTeamGoals,

                    AwayTeamId = awayTeamId,
                    AwayTeamGoals = awayTeamGoals,

                };
                matches.Add(match);
            }
            int numberOfTeams = GetNumberOfTeamsbyLeague(leagueId);
            int numberOfQueues = matches.Count / (numberOfTeams / 2);
            int numberOfMatchesInSingleQueue = numberOfTeams / 2;

            int skipMatches = 0;
            int actualQueue = 1;
            for (int i = 0; i < numberOfQueues; i++)
            {
                var matchQ = matches.Skip(skipMatches).Take(numberOfMatchesInSingleQueue);
                foreach (var match in matchQ)
                {
                    match.QueueNumber = actualQueue;
                }
                skipMatches += numberOfMatchesInSingleQueue;
                actualQueue += 1;
            }

            return matches;
        }
        private List<Team> GetTeamsByLeagueId(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _context.Teams.Where(x => x.LeagueId == leagueId).ToList();
        }
        private int GetNumberOfTeamsbyLeague(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));
            return _context.Teams.Where(x => x.LeagueId == leagueId).Count();
        }
        private List<Match> GetMatchesByLeagueId(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _context.Matches.Where(x => x.AwayTeam.LeagueId == leagueId || x.HomeTeam.LeagueId == leagueId).Include(x => x.AwayTeam).ToList();
        }
        private int GetCurrentQueue(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            var days = new DateTime().GetCurrentWeekDays().Select(d => d.Day).ToList();
            int month = DateTime.Today.Month;

            return _context.Matches
                .Where(z => z.AwayTeam.LeagueId == leagueId)
                .Where(x => x.Date.Value.Month == month && days.Contains(x.Date.Value.Day))
                .Select(q => q.QueueNumber)
                .FirstOrDefault();
        }
        private IQueryable<Match> GetMatchesByLeagueAndQueue(int leagueId, int queue)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _context.Matches
                .Where(x => x.QueueNumber == queue)
                .Where(z => z.AwayTeam.LeagueId == leagueId);
        }
        private int GetLastQueueNumber(int leagueId, bool isActual = false)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            if (isActual)
                return _context.Matches
                .Where(x => x.AwayTeam.LeagueId == leagueId && x.Date != null)
                .Max(x => x.QueueNumber);

            return _context.Matches
                .Where(x => x.AwayTeam.LeagueId == leagueId)
                .Max(x => x.QueueNumber);
        }
        private List<MatchQueueDTO> SetOrder(List<MatchQueueDTO> model)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return model.OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();
        }
        public bool AnyMatchesExists(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _context.Matches.Where(x => x.AwayTeam.LeagueId == leagueId).Any();
        }

        public List<MatchQueueDTO> GetMatchesFromToday(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            var today = DateTime.Today.Day;
            var month = DateTime.Today.Month;

            var matches = _context.Matches.Where(x => x.AwayTeam.LeagueId == leagueId && x.Date.Value.Day == today && x.Date.Value.Month == month);

            return MapToMatchQueueDTO(matches);
        }
        public List<TeamStatsDTO> GetTable(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            var result = GetStatsTotable(leagueId).OrderByDescending(x => x.Points).ToList();
            return result;
        }
        private List<TeamStatsDTO> GetStatsTotable(int leagueId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            var matches = GetMatchesByLeagueId(leagueId);

            List<int> teamsIds = matches.Select(x => x.AwayTeamId.Value).Distinct().ToList();

            List<TeamStatsDTO> teamStats = new List<TeamStatsDTO>();
            foreach (var teamId in teamsIds)
            {
                var matchesByTeam = matches.Where(x => x.HomeTeamId == teamId || x.AwayTeamId == teamId).ToList();

                int wins = CountWins(matchesByTeam, teamId);
                int loses = CountLoses(matchesByTeam, teamId);
                int draws = CountDraws(matchesByTeam);

                teamStats.Add(new TeamStatsDTO
                {
                    TeamName = matchesByTeam.Where(x => x.HomeTeamId == teamId).Select(x => x.HomeTeam.Name).FirstOrDefault(), //TODO
                    MatchesQuantity = wins + loses + draws,
                    Points = CountTotalPoints(wins, draws),
                    Wins = wins,
                    Loses = loses,
                    Draws = draws,
                    GoalsScored = CountTotalGoalsScored(matchesByTeam, teamId),
                    GoalsLoses = CountTotalGoalsLost(matchesByTeam, teamId)
                });
            }
            return teamStats;
        }
        private int CountTotalPoints(int wins, int draws)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return wins * 3 + draws * 1;
        }
        private int CountDraws(List<Match> matches)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            int totalDraws = 0;
            foreach (var match in matches)
            {
                //nie trzeba team id bo szukam po prostu takich samych wartosci
                if (match.HomeTeamGoals.HasValue)
                    if (match.HomeTeamGoals == match.AwayTeamGoals)
                        totalDraws++;
            }

            return totalDraws;
        }
        private int CountLoses(List<Match> matches, int teamId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            int homeLoses = CountHomeLoses(matches, teamId);
            int awayLoses = CountAwayLoses(matches, teamId);

            return homeLoses + awayLoses;
        }
        private int CountHomeLoses(List<Match> matches, int teamId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            int homeLoses = 0;
            foreach (var match in matches)
            {
                if (match.HomeTeamId == teamId && match.HomeTeamGoals.HasValue)
                    if (match.HomeTeamGoals < match.AwayTeamGoals)
                        homeLoses++;
            }
            return homeLoses;
        }
        private int CountAwayLoses(List<Match> matches, int teamId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            int awayLoses = 0;
            foreach (var match in matches)
            {
                if (match.AwayTeamId == teamId && match.AwayTeamGoals.HasValue)
                    if (match.AwayTeamGoals < match.HomeTeamGoals)
                        awayLoses++;
            }
            return awayLoses;
        }
        private int CountWins(List<Match> matches, int teamId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            int homeWins = CountHomeWins(matches, teamId);
            int awayWins = CountAwayWins(matches, teamId);

            return homeWins + awayWins;
        }
        private int CountHomeWins(List<Match> matches, int teamId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            int homeWins = 0;
            foreach (var match in matches)
            {
                if (match.HomeTeamId == teamId && match.HomeTeamGoals.HasValue)
                    if (match.HomeTeamGoals > match.AwayTeamGoals)
                        homeWins++;
            }
            return homeWins;
        }

        private int CountAwayWins(List<Match> matches, int teamId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            int awayWins = 0;
            foreach (var match in matches)
            {
                if (match.AwayTeamId == teamId && match.AwayTeamGoals.HasValue)
                    if (match.AwayTeamGoals > match.HomeTeamGoals)
                        awayWins++;
            }
            return awayWins;
        }
        private int CountTotalGoalsScored(List<Match> matches, int teamId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            int totalScored = 0;
            foreach (var match in matches)
            {
                if (match.HomeTeamId == teamId && match.HomeTeamGoals.HasValue)
                    totalScored += match.HomeTeamGoals.Value;

                if (match.AwayTeamId == teamId && match.AwayTeamGoals.HasValue)
                    totalScored += match.AwayTeamGoals.Value;
            }

            return totalScored;
        }
        private int CountTotalGoalsLost(List<Match> matches, int teamId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            int totalLost = 0;
            foreach (var match in matches)
            {
                if (match.HomeTeamId == teamId && match.AwayTeamGoals.HasValue)
                    totalLost += match.AwayTeamGoals.Value;

                if (match.AwayTeamId == teamId && match.HomeTeamGoals.HasValue)
                    totalLost += match.HomeTeamGoals.Value;
            }

            return totalLost;
        }
    }
}
