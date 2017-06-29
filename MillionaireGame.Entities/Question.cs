using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MillionaireGame.Entities
{
    public class Question: ICloneable
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public virtual QuestionStatistic QuestionStatistic { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public object Clone()
        {
            List<Answer> answers = new List<Answer>(4);
            foreach (var item in Answers)
            {
                answers.Add(item.Clone() as Answer);
            }
            return new Question
            {
                Title = Title,
                QuestionStatistic = QuestionStatistic,
                Answers = answers
            };
        }

        public bool IsAnswerCorrect(string answer)
        {
            var finded = Answers.Where(a => a.Title.Equals(answer)).FirstOrDefault();
            return finded == null ? false : finded.Correct;
        }
    }
}