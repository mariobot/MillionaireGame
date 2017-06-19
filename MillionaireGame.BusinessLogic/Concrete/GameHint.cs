using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillionaireGame.BusinessLogic.Abstract;
using MillionaireGame.Entities;

namespace MillionaireGame.BusinessLogic.Concrete
{
    public class GameHint : IGameHint
    {
        private readonly IMessageService _messageService;

        public GameHint(IMessageService messageService)
        {
            _messageService = messageService;
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
    }
}
