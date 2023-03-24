using Common.CustomNaming;
using Interfaces.Facades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringThesis.Api.Controllers
{
    public class TeamStatistics : BaseApiController
    {
        private readonly ILeagueFcd _leagueFcd;
        private readonly IMatchFcd _matchFcd;

        public TeamStatistics(ILeagueFcd leagueFcd, IMatchFcd matchFcd)
        {
            _leagueFcd = leagueFcd;
            _matchFcd = matchFcd;
        }
        [HttpGet("list")]
        public IActionResult GetTable(int leagueId)
        {
            if (!_leagueFcd.LeagueExists(leagueId))
                return BadRequest(CustomException.LeagueDoesNotExistsInDb);

            try
            {
                return Ok(_matchFcd.GetTable(leagueId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
