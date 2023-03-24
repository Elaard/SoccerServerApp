using Common.ModelsDTO.HtmlTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringThesis.Api.Helpers.Abstract
{
    public interface ITeamMetaHelper
    {
        Task<List<MetaTeamDTO>> GetMetaTeamData(string url);
    }
}
