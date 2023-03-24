using Common.CustomNaming;
using Common.Enums;
using Interfaces.Facades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringThesis.Api.Controllers
{
    public class MatchController:BaseApiController
    {
        private readonly IMatchFcd _matchFcd;
        private readonly ILeagueFcd _leagueFcd;

        public MatchController(IMatchFcd matchFcd, ILeagueFcd leagueFcd)
        {
            _matchFcd = matchFcd;
            _leagueFcd = leagueFcd;
        }
        [HttpGet("queue/{option}/{leagueId}/{queue?}")]
        public IActionResult GetQueue([FromRoute]int option, [FromRoute] int leagueId, [FromRoute] int? queue)
        {
            if (!_leagueFcd.LeagueExists(leagueId))
                return BadRequest(CustomException.LeagueDoesNotExistsInDb);

            switch (option)
            {
                case 1:
                    {
                        if (!queue.HasValue)
                            return BadRequest();
                        return Ok(_matchFcd.GetPreviousQueue(leagueId, queue.Value));
                    }
                case 2:
                    {
                        return Ok(_matchFcd.GetActualQueue(leagueId));
                    }
                case 3:
                    {
                        if (!queue.HasValue)
                            return BadRequest();
                        return Ok(_matchFcd.GetNextQueue(leagueId, queue.Value));
                    }
            }
            return BadRequest();
        }
        [HttpGet("today/list")]
        public IActionResult GetMatchesFromToday(int leagueId)
        {
            if (!_leagueFcd.LeagueExists(leagueId))
                return BadRequest(CustomException.LeagueDoesNotExistsInDb);

            try
            {
               return Ok(_matchFcd.GetMatchesFromToday(leagueId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
