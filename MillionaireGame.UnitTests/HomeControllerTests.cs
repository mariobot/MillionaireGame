using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using MillionaireGame.Entities;
using MillionaireGame.Frontend.Controllers;
using MillionaireGame.Repositories.Abstract;
using Moq;
using NUnit.Framework;

namespace MillionaireGame.UnitTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<IQuestionRepository> _questionsMock;
        private Mock<IGameStepRepository> _stepsMock;

        [SetUp]
        public void Init()
        {
            _questionsMock = new Mock<IQuestionRepository>();
            _questionsMock.Setup(q => q.Questions).Returns(new List<Question>
            {
                new Question
                {
                    Answers = new List<Answer>
                    {
                        new Answer { Title = "1_1", Correct = false },
                        new Answer { Title = "1_2", Correct = true },
                        new Answer { Title = "1_3", Correct = false },
                        new Answer { Title = "1_4", Correct = false }
                    },
                    Title = "Question_title_1"
                },
                new Question
                {
                    Answers = new List<Answer>
                    {
                        new Answer { Title = "2_1", Correct = false },
                        new Answer { Title = "2_2", Correct = false },
                        new Answer { Title = "2_3", Correct = false },
                        new Answer { Title = "2_4", Correct = true }
                    },
                    Title = "Question_title_2"
                }
            });

            _stepsMock = new Mock<IGameStepRepository>();
            _stepsMock.Setup(s => s.GameSteps).Returns(new List<GameStep>
            {
                new GameStep {Reward = 100, Step = 1 },
                new GameStep { Reward = 200, Step = 2 },
                new GameStep { Reward = 300, Step = 3 }
            });
        }

        [Test]
        public void HomeController_ReturnsIndexView()
        {
            var controller = new HomeController(null, null);

            var result = controller.Index() as ViewResult;

            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void HomeController_SaveUserSession()
        {
            var controller = new HomeController(_questionsMock.Object, _stepsMock.Object);
            var context = new Mock<HttpContext>();

            var result = controller.Game("talon") as ViewResult;

            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
