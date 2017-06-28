using MillionaireGame.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillionaireGame.Entities;

namespace MillionaireGame.Repositories.Concrete
{
    public class DbQuestionStatisticRepository : IQuestionStatisticRepository
    {
        private LoggerContext _context;

        public DbQuestionStatisticRepository(LoggerContext context)
        {
            _context = context;
        }

        public void Add(QuestionStatistic item)
        {
            _context.QuestionStatistics.Add(item);
        }

        public QuestionStatistic Find(int id)
        {
            return _context.QuestionStatistics.Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
