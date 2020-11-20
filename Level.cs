using System.Collections.Generic;

namespace Adventure
{
    public class Level
    {
        // JSON populated data
        public string name  { get; set; }
        public int    width { get; set; } 
        public int    height { get; set; }

        public LevelPosition spawnPosition { get; set; }
        public Dictionary<string, string> objects { get; set; }

        // local data
        Player player;

        public void Initilize(Player player)
        {
            this.player = player;
            player.SetPosition(spawnPosition);
        }

        public void MovePlayer(LevelPosition distance)
        {
            int newX = player.position.x + distance.x;
            int newY = player.position.y + distance.y;
            if (0 > newX || width-1 < newX)
                return;
            if (0 > newY || height-1 < newY)
                return;

            player.SetPosition(new LevelPosition(newX, newY));
            if (objects.ContainsKey(distance.ToString()))
            {
                // interact with object
            }
        }

        public override string ToString()
        {
            string display = "";

            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    string coord = $"{y},{x}";

                    if ($"{player.position.x},{player.position.y}" == coord)
                    {
                        display += " @ ";
                    }
                    else if (objects.ContainsKey(coord))
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

    public class LevelPosition
    {
        public int x { get; set; }
        public int y { get; set; }

        public LevelPosition() { }
        public LevelPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{x},{y}";
        }
    }
}