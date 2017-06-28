using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillionaireGame.Entities
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Correct { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}