using System.Collections.Generic;

namespace Adventure
{
    public class World
    {
        public Player player { get; set; }
        public List<Actor> actors { get; set; }
        public List<Level> levels { get; set; }
    }
}