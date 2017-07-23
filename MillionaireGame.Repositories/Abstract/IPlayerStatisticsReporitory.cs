using System.Collections.Generic;
using MillionaireGame.Entities;

namespace MillionaireGame.Repositories.Abstract
{
    public interface IPlayerStatisticsReporitory
    {
        void Add(PlayerStatistic playerStatistic);
        IEnumerable<PlayerStatistic> PlayerStatistics { get; }
    }
}
