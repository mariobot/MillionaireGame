using System;

namespace MillionaireGame.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public GameStep Step { get; set; }
        public DateTime BeginningGame { get; } = DateTime.Now;
    }
}