using System.Collections.Generic;
using MillionaireGame.Entities;
using MillionaireGame.Repositories.Abstract;

namespace MillionaireGame.Repositories.Concrete
{
    public class DbPlayerStatisticsRepository : IPlayerStatisticsReporitory
    {
        private readonly LoggerContext _context;

        public DbPlayerStatisticsRepository(LoggerContext context)
        {
            _context = context;
        }

        public void Add(PlayerStatistic playerStatistic)
        {
            _context.PlayerStatistics.Add(playerStatistic);
            _context.SaveChanges();
        }

        public IEnumerable<PlayerStatistic> PlayerStatistics => _context.PlayerStatistics;
    }
}
