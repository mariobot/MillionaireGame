using MillionaireGame.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using MillionaireGame.Entities;
using MillionaireGame.Repositories.Abstract;

namespace MillionaireGame.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IGameStepRepository _gameStepRepository;

        public HomeController(IQuestionRepository questionRepo, IGameStepRepository gameStepRepo)
        {
            _questionRepository = questionRepo;
            _gameStepRepository = gameStepRepo;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Game(string PlayerName)
        {
            Session["name"] = PlayerName;
            var questions = _questionRepository.Questions;
            var steps = _gameStepRepository.GameSteps;
            return View(questions);
        }
    }
}