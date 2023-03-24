using Common.Helpers.HtmlConverter;
using Common.ModelsDTO.HtmlTables;
using EngineeringThesis.Api.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringThesis.Api.Helpers
{
    public class TeamMetaHelper: ITeamMetaHelper
    {
        private TeamMetaConverter _converter;

        public TeamMetaHelper()
        {
            _converter = new TeamMetaConverter();
        }
        public async Task<List<MetaTeamDTO>> GetMetaTeamData(string url)
        {
            await _converter.HandleHtmlAsync(url);
            return await _converter.GetMetaTeamData();
        }
    }
}
