using AutoMapper;
using Common.CustomNaming;
using Common.ModelsDTO.HtmlTables;
using Infrastructure.EFCore.Models;
using Interfaces.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.EFCore.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LeagueService(Context context, IMapper mapper, ILogger<LeagueService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public int AddLeague(LeagueDTO model)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            //zmienic w automapperze
            League league = new League
            {
                Title = model.Title,
                UrlForHtml = model.UrlForHtml
            };

            _context.Add(league);
            _context.SaveChanges();

            return league.Id;
        }
        public LeagueDTO GetMappedLeagueByHtml(string html)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _mapper.Map<LeagueDTO>(GetLeagueByHtml(html));
        }

        public LeagueDTO GetMappedLeagueById(int urlId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _mapper.Map<LeagueDTO>(GetLeagueById(urlId));
        }
        public List<LeagueDTO> GetMappedLeagues()
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _mapper.Map<List<LeagueDTO>>(GetLeagues());
        }
        public List<League> GetLeagues()
        {
            return _context.Leagues.ToList();
        }

        public int RemoveLeague(int id)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            var league = GetLeagueById(id);

            _context.Leagues.Remove(league);
            _context.SaveChanges();

            return league.Id;
        }

        public bool LeagueExists(int id)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _context.Leagues.Any(x => x.Id == id);
        }

        public bool LeagueExists(string title)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _context.Leagues.Any(x => x.Title == title);
        }

        public bool IsAnyLeagueExists()
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _context.Leagues.Any();
        }
        private League GetLeagueByHtml(string html)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _context.Leagues.FirstOrDefault(x => x.UrlForHtml == html);
        }

        private League GetLeagueById(int urlId)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            _logger.LogInformation(CustomLogInformation.LogInfo(m.Name));

            return _context.Leagues.FirstOrDefault(x => x.Id == urlId);
        }
      
    }
}
