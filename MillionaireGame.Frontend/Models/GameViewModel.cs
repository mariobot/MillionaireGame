using MillionaireGame.Entities;
using System.Collections.Generic;

namespace MillionaireGame.Frontend.Models
{
    public class GameViewModel
    {
        public int QuestionIndex { get; set; }

        public Question Question { get; set; }

        public IEnumerable<GameStep> GameSteps { get; set; }
    }
}