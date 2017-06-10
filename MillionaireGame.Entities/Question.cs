using System.Collections.Generic;

namespace MillionaireGame.Entities
{
    public class Question
    {
        public string Title { get; set; }
        public IEnumerable<Answer> Answers { get; set; }

        public Question(string question, IEnumerable<Answer> answers)
        {
            Title = question;
            Answers = answers;
        }
    }
}