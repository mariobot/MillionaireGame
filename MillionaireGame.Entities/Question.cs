using System.Collections.Generic;
using System.Linq;

namespace MillionaireGame.Entities
{
    public class Question
    {
        public string Title { get; set; }
        public IEnumerable<Answer> Answers { get; set; }

        public bool IsAnswerCorrect(string answer)
        {
            var finded = Answers.Where(a => a.Title.Equals(answer)).FirstOrDefault();
            return finded == null ? false : finded.Correct;
        }
    }
}