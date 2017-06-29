using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillionaireGame.Entities
{
    public class Answer: ICloneable
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Correct { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        [JsonIgnore]
        public virtual Question Question { get; set; }

        public object Clone()
        {
            return new Answer
            {
                Title = Title.Clone().ToString(),
                Correct = Correct
            };
        }
    }
}