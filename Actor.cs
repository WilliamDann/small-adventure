using System.Collections.Generic;

namespace Adventure
{
    public class Actor
    {
        public int hp           { get; set; }
        public int maxHP        { get; set; }
        public string name      { get; set; }

        public string character { get; set; }

        public List<string> lines { get; set; }
    }
}