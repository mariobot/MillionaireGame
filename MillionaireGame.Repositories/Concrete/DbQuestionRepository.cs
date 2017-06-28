using MillionaireGame.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;
using MillionaireGame.Entities;

namespace MillionaireGame.Repositories.Concrete
{
    public class DbQuestionRepository : IQuestionRepository
    {
        private LoggerContext _context;

        public DbQuestionRepository(LoggerContext context)
        {
            _context = context;
        }

        public IEnumerable<Question> Questions
        {
            get
            {
                return _context.Questions.AsEnumerable();
            }
        }
    }
}
