using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillionaireGame.Entities
{
    public class QuestionStatistic
    {
        public int QuestionCounter { get; set; }
        [Key, ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<AnswerStatistic> AnswerStatistics { get; set; }
    }
}
