using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillionaireGame.BusinessLogic.Abstract;
using MillionaireGame.Entities;
using MillionaireGame.Repositories.Abstract;

namespace MillionaireGame.BusinessLogic.Concrete
{
    public class GameHint : IGameHint
    {
        private readonly IMessageService _messageService;
        private readonly IQuestionStatisticRepository _questionStatisticRepository;

        public GameHint(IMessageService messageService, IQuestionStatisticRepository questionStatisticRepository)
        {
            _messageService = messageService;
            _questionStatisticRepository = questionStatisticRepository;
        }

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
                    outIndex = rnd.Next(0, 3);
                }
                else
                {
                    answer.Title = string.Empty;
                }
            }
            return question;
        }

        public void FriendCallHint(Question question, string playerName, string recipient)
        {
            var sb = new StringBuilder(200);
            sb.Append($"Привіт, {recipient}!\n");
            sb.Append($"Твій друг {playerName} зараз грає у " +
                      "'Хто хоче стати мільйонером' і потребує твоєї допомоги!\n");
            sb.Append($"Питання: {question.Title}\n");
            sb.Append("Варіанти відповідей: \n");
            foreach (var answer in question.Answers)
            {
                sb.Append($" * {answer.Title}\n");
            }

            new Task(() => _messageService.SendMessage(sb.ToString(), recipient)).Start();
        }

        public string AudienceHint(Question question)
        {
            var url = @"https://google.com/#q=" + question.Title;
            return url;
        }

        public ICollection<AudienceHintResult> AudienceHintWithStatistic(Question question)
        {
            var answerStatistic = _questionStatisticRepository.Find(question.Id).AnswerStatistics;
            var result = new List<AudienceHintResult>(4);

            if (answerStatistic == null)
            {
                throw new ArgumentNullException();
            }

            var totalAnswersCount = 0;
            for (int i = 0; i < 4; i++)
            {
                totalAnswersCount += answerStatistic.ElementAt(i).AnswerCounter;
            }

            // need to be replaced by smarter algorithm :D
            if (totalAnswersCount == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    result.Add(new AudienceHintResult
                    {
                        AnswerTitle = question.Answers.ElementAt(i).Title,
                        AudienceRate = 25
                    });
                }
                return result;
            }

            var scale = 100 / totalAnswersCount;

            for (int i = 0; i < 4; i++)
            {
                result.Add(new AudienceHintResult
                {
                    AnswerTitle = question.Answers.ElementAt(i).Title,
                    AudienceRate = answerStatistic.ElementAt(i).AnswerCounter * scale
                });
            }

            return result;
        }
    }
}
