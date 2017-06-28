using MillionaireGame.Frontend.Models;
using MillionaireGame.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillionaireGame.Frontend.Filters
{
    public class ActionLoggerAttribute : FilterAttribute
    {

    }

    public class ActionLoggerFilter : IActionFilter
    {
        private IQuestionStatisticRepository _questionStatisticRepository;

        public ActionLoggerFilter(IQuestionStatisticRepository repository)
        {
            _questionStatisticRepository = repository;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            PlayerAnswerViewModel answer = filterContext.Controller.ViewBag.Answer;
            var questionStatistic = _questionStatisticRepository.Find(answer.QuestionIndex + 1);
            if (questionStatistic == null)
            {
                return;
            }
            questionStatistic.QuestionCounter++;
            int answerId = questionStatistic.Question.Answers
                .First(a => a.Title == answer.PlayerAnswer).Id;
            var answerStatistic = questionStatistic.AnswerStatistics
                .First(a => a.Id == answerId);
            answerStatistic.AnswerCounter++;
            _questionStatisticRepository.SaveChanges();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }
    }
}