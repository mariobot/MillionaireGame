using System.Collections.Generic;
using System.IO;
using MillionaireGame.Entities;
using MillionaireGame.Repositories.Abstract;
using Newtonsoft.Json;

namespace MillionaireGame.Repositories.Concrete
{
    public class JsonGameStepRepository : IGameStepRepository
    {
        private readonly string _filename;

        public IEnumerable<GameStep> GameSteps
        {
            get
            {
                string text;
                using (var reader = new StreamReader(_filename))
                {
                    text = reader.ReadToEnd();
                }
                var steps = JsonConvert.DeserializeObject<List<GameStep>>(text);

                return steps;
            }
        }

        public JsonGameStepRepository(string filename)
        {
            _filename = filename;
        }
    }
}
