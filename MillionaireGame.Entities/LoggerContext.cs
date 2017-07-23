using System.Data.Entity;

namespace MillionaireGame.Entities
{
    public class LoggerContext : DbContext
    {
        public LoggerContext(): base("LoggerConnection")
        {
         //   Database.Connection.ConnectionString = 
        }

        public DbSet<ExceptionDetail> ExceptionDetails { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionStatistic> QuestionStatistics { get; set; }
        public DbSet<AnswerStatistic> AnswerStatistics { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
    }
}
