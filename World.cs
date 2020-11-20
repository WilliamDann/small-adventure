using System.Collections.Generic;

namespace Adventure
{
    public class World
    {
        public Player player { get; set; }
        
        public Dictionary<string, Actor> actors { get; set; }
        public Dictionary<string, Level> levels { get; set; }
    }
}