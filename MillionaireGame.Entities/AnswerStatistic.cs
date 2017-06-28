using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillionaireGame.Entities
{
    public class AnswerStatistic
    {
        [Key]
        public int Id { get; set; }
        public int AnswerCounter { get; set; }
        [ForeignKey("QuestionStatistic")]
        public int QuestionStatisticId { get; set; }
        public virtual QuestionStatistic QuestionStatistic { get; set; }
    }
}
