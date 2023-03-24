using Interfaces.Infrastructure;

namespace Infrastructure.EFCore.Services
{
    public class CoachService : ICoachService
    {
        private readonly Context context;
        public CoachService(Context context)
        {
            this.context = context;
        }
    }
}
