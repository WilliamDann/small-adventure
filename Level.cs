using System.Collections.Generic;

namespace Adventure
{
    public class Level
    {
        public string name  { get; set; }
        public int    width { get; set; } 
        public int    height { get; set; }
        public Dictionary<string, string> objects { get; set; }

        public override string ToString()
        {
            string display = "";

            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    string coord = $"{y},{x}";
                    if (objects.ContainsKey(coord))
                    {
                        display += $" {objects[coord]} ";
                    } else 
                    {
                        display += " _ ";
                    }

                }
                display += "\n";
            }

            return display;
        }
    }
}