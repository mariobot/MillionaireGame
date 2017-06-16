using NUnit.Framework;
using MillionaireGame.Entities;
using System.Collections.Generic;
using MillionaireGame.BusinessLogic.Concrete;
using System.Linq;

namespace MillionaireGame.UnitTests
{
    [TestFixture]
    public class GameHintTests
    {
        [Test]
        public void GameHint_FiftyPercentsHintTest()
        {
            var question = new Question()
            {
                Title = "q title",
                Answers = new List<Answer>
                    {
                        new Answer{Title = "a1", Correct = false},
                        new Answer{Title = "a2", Correct = false},
                        new Answer{Title = "a3", Correct = false},
                        new Answer{Title = "a4", Correct = true},
                    }
            };
            var gameHint = new GameHint();

            var newQuestion = gameHint.FiftyPercentsHint(question);

            Assert.IsTrue(IsQuestionWithHintValid(newQuestion));
        }

        private bool IsQuestionWithHintValid(Question question)
        {
            try
            {
                bool result = false;
                var allowAnswers = question.Answers.Where(a => !string.IsNullOrEmpty(a.Title));
                result = allowAnswers.Any(a => a.Correct)
                    || allowAnswers.Count() == 2;
                return result;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
