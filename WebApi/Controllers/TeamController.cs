using Interfaces.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringThesis.Api.Controllers
{
    public class TeamController:BaseApiController
    {

        private readonly ILeagueFcd _leagueFcd;

        public TeamController(ILeagueFcd leagueFcd)
        {
            _leagueFcd = leagueFcd;
        }
    }
}
