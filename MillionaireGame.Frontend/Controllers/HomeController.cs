using MillionaireGame.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using MillionaireGame.Entities;

namespace MillionaireGame.Frontend.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private readonly JsonQuestionRepository _repo =
            new JsonQuestionRepository(HostingEnvironment.MapPath("~/App_Data/Questions.json"));

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Game(string PlayerName)
        {
            Session["name"] = PlayerName;
            var questions = _repo.Questions;
            return View(questions);
        }
    }
}