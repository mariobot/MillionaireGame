using MillionaireGame.BusinessLogic.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillionaireGame.Entities;

namespace MillionaireGame.BusinessLogic.Concrete
{
    public class GameHint : IGameHint
    {
        public Question FiftyPercentsHint(Question question)
        {
            Random rnd = new Random();
            int outIndex = rnd.Next(0, 3);
            for (int i = 0; i < 2; i++)
            {
                var answer = question.Answers.ElementAt(outIndex);
                if (answer.Correct || string.IsNullOrEmpty(answer.Title))
                {
                    i--;
                }
                else
                {
                    answer.Title = "";
                }
            }
            return question;
        }
    }
}
