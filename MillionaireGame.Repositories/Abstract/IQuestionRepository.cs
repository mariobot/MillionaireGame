using MillionaireGame.Entities;
using System.Collections.Generic;

namespace MillionaireGame.Repositories.Abstract
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> Questions { get; }
    }
}
