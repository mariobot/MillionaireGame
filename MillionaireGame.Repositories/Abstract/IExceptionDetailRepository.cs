using MillionaireGame.Entities;

namespace MillionaireGame.Repositories.Abstract
{
    public interface IExceptionDetailRepository
    {
        void Add(ExceptionDetail exceptionDetail);
        void SaveChanges();
    }
}
