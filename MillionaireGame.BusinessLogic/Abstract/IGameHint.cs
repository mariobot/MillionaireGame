using MillionaireGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionaireGame.BusinessLogic.Abstraction
{
    public interface IGameHint
    {
        /// <summary>
        /// Implements fifty on fifty hint.
        /// </summary>
        /// <param name="question">Initial question</param>
        /// <returns>Question that contains only two titled answers, other should be empty string</returns>
        Question FiftyPercentsHint(Question question);

        //TODO: Add friend call hint
    }
}
