using System.Collections.Generic;
using MillionaireGame.Entities;

namespace MillionaireGame.Repositories.Abstract
{
    public interface IGameStepRepository
    {
        IEnumerable<GameStep> GameSteps { get; }
    }
}
