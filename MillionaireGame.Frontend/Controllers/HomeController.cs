﻿using System;
using System.Linq;
using System.Web.Mvc;
using MillionaireGame.BusinessLogic.Abstract;
using MillionaireGame.Repositories.Abstract;
using MillionaireGame.Frontend.Models;
using Newtonsoft.Json;
using MillionaireGame.Frontend.Filters;
using MillionaireGame.Entities;

namespace MillionaireGame.Frontend.Controllers
{
    [ExceptionLogger]
    public class HomeController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IGameStepRepository _gameStepRepository;
        private readonly IPlayerStatisticsReporitory _playerStatisticsReporitory;
        private readonly IGameHint _gameHint;

        public HomeController(IQuestionRepository questionRepo, IGameStepRepository gameStepRepo,
           IPlayerStatisticsReporitory statisticsRepo, IGameHint gameHint)
        {
            _questionRepository = questionRepo;
            _gameStepRepository = gameStepRepo;
            _playerStatisticsReporitory = statisticsRepo;
            _gameHint = gameHint;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Game(string playerName)
        {
            Session["name"] = playerName;

            var game = new GameViewModel
            {
                EndOfGame = false,
                QuestionIndex = 0,
                Question = _questionRepository.Questions.ElementAt(0),
                GameSteps = _gameStepRepository.GameSteps.OrderByDescending(i => i.Reward)
                //Question = _questionRepository.Questions.First(),
                //GameSteps = _gameStepRepository.GameSteps.OrderByDescending(i => i.Reward)
            };

            return View(game);
        }

        [HttpPost]
        [ActionLogger]
        public ActionResult PlayerGame(PlayerAnswerViewModel answer)
        {
            ViewBag.Answer = answer;
            int index = answer.QuestionIndex;
            var currentQuestion = _questionRepository.Questions.ElementAt(index); //see lower
            bool endOfGame = true;
            if (currentQuestion.IsAnswerCorrect(answer.PlayerAnswer))
            {
                endOfGame = false;
            }
            else
            {
                if (index == 14)
                {
                    index--;
                }
            }
            if (index == 14)
            {
                endOfGame = true; //assign true if the last answer was correct
            }
            int newIndex = index + 1;
            var game = new GameViewModel
            {
                EndOfGame = endOfGame,
                QuestionIndex = newIndex,                              //uncomment when all quests will be able
                Question = newIndex == 15 ? null : _questionRepository.Questions.ElementAt(newIndex), //ElementAt(newIndex)  
                GameSteps = null
            };
            string response = JsonConvert.SerializeObject(game);
            return Json(response);
        }

        // !!!!ATTENTION!!!! behaviour of game result was changed  
        [HttpPost]
        public ActionResult GameResult(int step) //step need for win sum of money
        {
            //method that implements unburned sum result should be ran here
            //and the step should be passed to the following ViewBag property

            var reward = 0;

            if (step >= 6 && step < 11)
            {
                reward = _gameStepRepository.GameSteps.ElementAt(4).Reward;
            }
            else if (step >= 11 && step < 15)
            {
                reward = _gameStepRepository.GameSteps.ElementAt(9).Reward;
            }
            else if (step == 15)
            {
                reward = _gameStepRepository.GameSteps.ElementAt(14).Reward;
            }
            ViewBag.Step = step;
            ViewBag.Reward = reward;

            // save result statistics for current player
            _playerStatisticsReporitory.Add(new PlayerStatistic
            {
                Name = Session["name"].ToString(),
                Result = reward,
                ResultDateTime = DateTime.Now
            });

            return View();
        }

        [HttpPost]
        public JsonResult FiftyPercentsHint(int questionIndex)
        {
            var question = _questionRepository.Questions.ElementAt(questionIndex).Clone() as Question;
            var hintQuestion = _gameHint.FiftyPercentsHint(question);
            string strQuestion = JsonConvert.SerializeObject(hintQuestion);
            return Json(strQuestion);
        }

        [HttpPost]
        public JsonResult FriendCallHint(int questionIndex, string recipient)
        {
            var question = _questionRepository.Questions.ElementAt(questionIndex);
            _gameHint.FriendCallHint(question, (string)Session["name"], recipient);
            return Json(recipient);
        }

        [HttpPost]
        public JsonResult AudienceHint(int questionIndex)
        {
            var question = _questionRepository.Questions.ElementAt(questionIndex);
            var url = _gameHint.AudienceHint(question);
            var result = JsonConvert.SerializeObject(url);
            return Json(result);
        }

        [HttpPost]
        public JsonResult AudienceHintWithStatistic(int questionIndex)
        {
            var question = _questionRepository.Questions.ElementAt(questionIndex);
            var audienceStatistics = _gameHint.AudienceHintWithStatistic(question);
            var strAudienceStatistics = JsonConvert.SerializeObject(audienceStatistics);
            return Json(strAudienceStatistics);
        }

        [HttpGet]
        public ViewResult Results()
        {
            return View(_playerStatisticsReporitory.PlayerStatistics);
        }
    }
}
