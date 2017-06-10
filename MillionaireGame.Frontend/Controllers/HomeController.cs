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
        // GET: Home
        private readonly IQuestionRepository _questionRepository;

        public HomeController(IQuestionRepository questionRepo)
        {
            _questionRepository = questionRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Game(string PlayerName)
        {
            Session["name"] = PlayerName;
            var questions = _questionRepository.Questions;
            return View(questions);
        }
    }
}