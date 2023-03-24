using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringThesis.Api.Helpers
{
    public interface IMainDataHelper
    {
        Task AddLeague(string url);
        void RemoveLeague(int leagueId);
        Task UpdateLeague(string url, int leagueId);
        Task UpdateLeagues();
    }
}
