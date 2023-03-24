using Common.CustomNaming;
using Common.ModelsDTO.HtmlTables;
using Interfaces.Facades;
using Interfaces.Infrastructure;
using System;
using System.Collections.Generic;

namespace Facades
{
    public class LeagueFcd : ILeagueFcd
    {
        private readonly ILeagueService _leagueSrv;

        public LeagueFcd(ILeagueService leagueSrv)
        {
            _leagueSrv = leagueSrv;
        }

        public int AddLeague(LeagueDTO league)
        {
            if (_leagueSrv.LeagueExists(league.Title))
            {
                throw new InvalidOperationException(CustomException.LeagueAlreadyExistsInDb);
            }
            return _leagueSrv.AddLeague(league);
        }
        public LeagueDTO GetMappedLeagueByHtml(string html)
        {
            return _leagueSrv.GetMappedLeagueByHtml(html);
        }

        public LeagueDTO GetMappedLeagueById(int urlId)
        {
            return _leagueSrv.GetMappedLeagueById(urlId);
        }

        public List<LeagueDTO> GetMappedLeagues()
        {
            return _leagueSrv.GetMappedLeagues();
        }

        public int RemoveLeague(int id)
        {
            if (!_leagueSrv.LeagueExists(id))
            {
                throw new InvalidOperationException(CustomException.LeagueDoesNotExistsInDb);
            }
            return _leagueSrv.RemoveLeague(id);
        }
        public bool LeagueExists(int id)
        {
            return _leagueSrv.LeagueExists(id);
        }

        public bool IsAnyLeagueExists()
        {
            return _leagueSrv.IsAnyLeagueExists();
        }
    }
}
