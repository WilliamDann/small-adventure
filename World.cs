using System.Collections.Generic;

namespace Adventure
{
    
    public class World
    {
        public Player player { get; set; }
        
        public Dictionary<string, Actor> actors { get; set; }
        public Dictionary<string, Level> levels { get; set; }

        public Level CurrentLevel { get; private set;}

        public void LoadLevel(string level)
        {
            if (!levels.ContainsKey(level))
                throw new KeyNotFoundException($"Level {level} does not exist");
        
            CurrentLevel = levels[level];
            CurrentLevel.Initilize(this);
        }
    }
}