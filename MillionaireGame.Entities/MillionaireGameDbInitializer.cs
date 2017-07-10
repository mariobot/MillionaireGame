using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using MillionaireGame.Entities.Migrations;
using Newtonsoft.Json;

namespace MillionaireGame.Entities
{
    public class MillionaireGameDbInitializer : CreateDatabaseIfNotExists<LoggerContext>
    {
        protected override void Seed(LoggerContext context)
        {
            base.Seed(context);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LoggerContext, Configuration>());

            string text;
            using (var reader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "Questions.json")))
            {
                text = reader.ReadToEnd();
            }
            var questions = JsonConvert.DeserializeObject<List<Question>>(text);

            foreach (var item in questions)
            {
                var answerStatistics = new List<AnswerStatistic>();

                var questionStatistic = new QuestionStatistic
                {
                    QuestionCounter = 0,
                    AnswerStatistics = answerStatistics
                };
                for (int i = 0; i < 4; i++)
                {
                    questionStatistic.AnswerStatistics.Add(new AnswerStatistic { AnswerCounter = 0 });
                }

                item.QuestionStatistic = questionStatistic;
                context.Questions.Add(item);
            }
            context.SaveChanges();
        }
    }
}
