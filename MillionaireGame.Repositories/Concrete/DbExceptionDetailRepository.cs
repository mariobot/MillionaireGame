using MillionaireGame.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillionaireGame.Entities;

namespace MillionaireGame.Repositories.Concrete
{
    public class DbExceptionDetailRepository : IExceptionDetailRepository
    {
        private LoggerContext _context;

        public DbExceptionDetailRepository(LoggerContext context)
        {
            _context = context;
        }

        public void Add(ExceptionDetail exceptionDetail)
        {
            _context.ExceptionDetails.Add(exceptionDetail);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
