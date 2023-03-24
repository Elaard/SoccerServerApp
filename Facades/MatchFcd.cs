using Common.CustomNaming;
using Common.ModelsDTO;
using Common.ModelsDTO.HtmlTables;
using Interfaces.Facades;
using Interfaces.Infrastructure;
using System;
using System.Collections.Generic;

namespace Facades
{
    public class MatchFcd : IMatchFcd
    {
        private readonly IMatchService _matchSrv;

        public MatchFcd(IMatchService matchSrv)
        {
            _matchSrv = matchSrv;
        }

        public void AddMatches(List<MatchDTO> model, int leagueId)
        {
            _matchSrv.AddMatches(model, leagueId);
        }

        public QueueDTO GetActualQueue(int leagueId)
        {
            if (!_matchSrv.AnyMatchesExists(leagueId))
            {
                throw new InvalidOperationException(CustomException.ThereIsNoMatches);
            }
            return _matchSrv.GetActualQueue(leagueId);
        }

        public List<MatchQueueDTO> GetMatchesFromToday(int leagueId)
        {
            if (!_matchSrv.AnyMatchesExists(leagueId))
            {
                throw new InvalidOperationException(CustomException.ThereIsNoMatches);
            }
            return _matchSrv.GetMatchesFromToday(leagueId);
        }

        public QueueDTO GetNextQueue(int leagueId, int queue)
        {
            if (!_matchSrv.AnyMatchesExists(leagueId))
            {
                throw new InvalidOperationException(CustomException.ThereIsNoMatches);
            }
            return _matchSrv.GetNextQueue(leagueId, queue);
        }

        public QueueDTO GetPreviousQueue(int leagueId, int queue)
        {
            if (!_matchSrv.AnyMatchesExists(leagueId))
            {
                throw new InvalidOperationException(CustomException.ThereIsNoMatches);
            }
            return _matchSrv.GetPreviousQueue(leagueId, queue);
        }

        public List<TeamStatsDTO> GetTable(int leagueId)
        {
            return _matchSrv.GetTable(leagueId);
        }

        public void RemoveMatchesByLeagueId(int leagueId)
        {
            if (!_matchSrv.AnyMatchesExists(leagueId))
            {
                throw new InvalidOperationException(CustomException.ThereIsNoMatches);
            }
            _matchSrv.RemoveMatchesByLeagueId(leagueId);
        }

        public void UpdateMatches(List<MatchDTO> matches, int leagueId)
        {
            _matchSrv.UpdateMatches(matches, leagueId);
        }
    }
}
