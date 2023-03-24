using Common.CustomNaming;
using EngineeringThesis.Api.Helpers;
using EngineeringThesis.Api.Helpers.Abstract;
using Interfaces.Facades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringThesis.Api.Controllers
{
    public class LeagueController : BaseApiController
    {
        private readonly ILeagueFcd _leagueFcd;
        private readonly IMainDataHelper _md;

        public LeagueController(ILeagueFcd leagueFcd, IMainDataHelper md, ITeamMetaHelper tm)
        {
            _leagueFcd = leagueFcd;
            _md = md;
        }
        [HttpGet("list")]
        public IActionResult Get()
        {
            try
            {
                var leagues = _leagueFcd.GetMappedLeagues();
                return Ok(leagues);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Add(string url)
        {
            try
            {
                if (String.IsNullOrEmpty(url))
                    return BadRequest(new { message = ApiException.IncorrectUrl });

                await _md.AddLeague(url);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(string url)
        {
            try
            {
                var league = _leagueFcd.GetMappedLeagueByHtml(url);

                if (league == null)
                    return BadRequest(new { message = ApiException.IncorrectUrl });

                await _md.UpdateLeague(league.UrlForHtml, league.Id);

                return Ok();
            }


            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("update/{leagueId}")]
        public async Task<IActionResult> Update(int leagueId)
        {
            try
            {
                var league = _leagueFcd.GetMappedLeagueById(leagueId);

                if (league == null)
                    return BadRequest(new { message = ApiException.IncorrectUrl });

                await _md.UpdateLeague(league.UrlForHtml, league.Id);

                return Ok();
            }


            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("updateLeagues")]
        public async Task<IActionResult> Update()
        {
            try
            {
                var leagues = _leagueFcd.GetMappedLeagues();

                if (leagues.Count == 0)
                    return BadRequest(new { message = ApiException.NoUrlsInDatabase });

                await _md.UpdateLeagues();

                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("delete/{leagueId}")]
        public IActionResult Remove(int leagueId)
        {
            if (!_leagueFcd.LeagueExists(leagueId))
                return BadRequest(new { message = CustomException.LeagueDoesNotExistsInDb });

            try
            {
                _md.RemoveLeague(leagueId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("delete/leagues")]
        public IActionResult RemoveAll()
        {

            try
            {
                var leagues = _leagueFcd.GetMappedLeagues();

                if (leagues.Count == 0)
                    return BadRequest(new { message = ApiException.NoUrlsInDatabase });

                foreach (var lg in leagues)
                    _md.RemoveLeague(lg.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
