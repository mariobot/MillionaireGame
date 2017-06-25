using System.Data.Entity;

namespace MillionaireGame.Entities
{
    public class LoggerContext : DbContext
    {
        public LoggerContext(): base("LoggerConnection")
        {
        }

        public DbSet<ExceptionDetail> ExceptionDetails { get; set; }
    }
}
