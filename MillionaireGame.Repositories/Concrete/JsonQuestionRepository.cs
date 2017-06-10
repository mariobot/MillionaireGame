using System.Collections.Generic;
using System.IO;
using MillionaireGame.Entities;
using MillionaireGame.Repositories.Abstract;
using Newtonsoft.Json;

namespace MillionaireGame.Repositories.Concrete
{
    public class JsonQuestionRepository : IQuestionRepository
    {
        private readonly string _filename;

        public IEnumerable<Question> Questions
        {
            get
            {
                string text;
                using (var reader = new StreamReader(_filename))
                {
                    text = reader.ReadToEnd();
                }
                var questions = JsonConvert.DeserializeObject<List<Question>>(text);

                return questions;
            }
        }

        public JsonQuestionRepository(string filename)
        {
            _filename = filename;
        }
    }
}