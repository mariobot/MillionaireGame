using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MillionaireGame.Entities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual QuestionStatistic QuestionStatistic { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public bool IsAnswerCorrect(string answer)
        {
            var finded = Answers.Where(a => a.Title.Equals(answer)).FirstOrDefault();
            return finded == null ? false : finded.Correct;
        }
    }
}