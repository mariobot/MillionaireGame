using MillionaireGame.Entities;

namespace MillionaireGame.Repositories.Abstract
{
    public interface IQuestionStatisticRepository
    {
        void Add(QuestionStatistic item);
        QuestionStatistic Find(int id);
        void SaveChanges();
    }
}
