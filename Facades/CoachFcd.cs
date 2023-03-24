using Interfaces.Facades;
using Interfaces.Infrastructure;

namespace Facades
{
    public class CoachFcd : ICoachFcd
    {
        private readonly ICoachService coachSrv;

        public CoachFcd(ICoachService coachSrv)
        {
            this.coachSrv = coachSrv;
        }
    }
}
