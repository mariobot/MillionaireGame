namespace MillionaireGame.Entities.Migrations
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MillionaireGame.Entities.LoggerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MillionaireGame.Entities.LoggerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            string text;                          //ConfigurationManager.AppSettings["QuestionsPath"] not working :(
            using (var reader = new StreamReader(@"D:\EpamLessons\Lesson_MVC_1\MillionaireGame\MillionaireGame.Frontend\App_Data\Questions.json"))
            {
                text = reader.ReadToEnd();
            }
            var questions = JsonConvert.DeserializeObject<List<Question>>(text);

            foreach (var item in questions)
            {
                var answerStatistics = new List<AnswerStatistic>();
                
                var questionStatistic = new QuestionStatistic()
                {
                    QuestionCounter = 0,
                    AnswerStatistics = answerStatistics
                };
                for (int i = 0; i < 4; i++)
                {
                    questionStatistic.AnswerStatistics.Add(new AnswerStatistic { AnswerCounter = 0 });
                }
                
                item.QuestionStatistic = questionStatistic;
                context.Questions.AddOrUpdate(item);
            };
            context.SaveChanges();
        }
    }
}
