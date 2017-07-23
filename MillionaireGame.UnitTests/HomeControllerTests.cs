using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MillionaireGame.Entities;
using MillionaireGame.Frontend.Controllers;
using MillionaireGame.Frontend.Models;
using MillionaireGame.Repositories.Abstract;
using MillionaireGame.UnitTests.Helpers;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MillionaireGame.UnitTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<IQuestionRepository> _questionsMock;
        private Mock<IGameStepRepository> _stepsMock;
        private HttpContextBase _fakeContext;

        [SetUp]
        public void InitRepositories()
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

        [SetUp]
        public void InitFakeContext()
        {
            _fakeContext = MockHttpSession.FakeHttpContext();
        }

        [Test]
        public void HomeController_ReturnsIndexView()
        {
            var controller = new HomeController(null, null, null, null);

            var result = controller.Index() as ViewResult;

            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void HomeController_ReturnsGameView()
        {
            var controller = new HomeController(_questionsMock.Object, _stepsMock.Object, null, null);
            controller.ControllerContext = new ControllerContext(_fakeContext, new RouteData(), controller);

            var result = controller.Game("TestPlayer") as ViewResult;

            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void HomeController_ReturnsPlayerGameResponse()
        {
            var controller = new HomeController(_questionsMock.Object, _stepsMock.Object, null, null);
            controller.ControllerContext = new ControllerContext(_fakeContext, new RouteData(), controller);
            var playerAnswerModel = new PlayerAnswerViewModel();

            var result = controller.PlayerGame(playerAnswerModel) as JsonResult;

            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void HomeController_ReturnsEndOfGameResponse_WithIncorrectAnswer()
        {
            var controller = new HomeController(_questionsMock.Object, _stepsMock.Object, null, null);
            controller.ControllerContext = new ControllerContext(_fakeContext, new RouteData(), controller);
            var playerAnswerModel = new PlayerAnswerViewModel
            {
                PlayerAnswer = "1_3",
                QuestionIndex = 0
            };

            var controllerResult = controller.PlayerGame(playerAnswerModel) as JsonResult;
            var gameViewModel = JsonConvert.DeserializeObject<GameViewModel>(controllerResult?.Data.ToString());

            Assert.AreEqual(true, gameViewModel.EndOfGame);
        }

        [Test]
        public void HomeController_ReturnsNextQuestionResponse_WithCorrectAnswer()
        {
            var controller = new HomeController(_questionsMock.Object, _stepsMock.Object, null, null);
            controller.ControllerContext = new ControllerContext(_fakeContext, new RouteData(), controller);
            var playerAnswerModel = new PlayerAnswerViewModel
            {
                PlayerAnswer = "1_2",
                QuestionIndex = 0
            };

            var controllerResult = controller.PlayerGame(playerAnswerModel) as JsonResult;
            var gameViewModel = JsonConvert.DeserializeObject<GameViewModel>(controllerResult?.Data.ToString());
            var repositoryQuestion = _questionsMock.Object.Questions.ElementAt(1);
            var modelQuestion = gameViewModel.Question;

            Assert.AreEqual(false, gameViewModel.EndOfGame);
            Assert.AreEqual(repositoryQuestion.Title, modelQuestion.Title);
        }
    }
}
