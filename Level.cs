using System.Collections.Generic;
using System;
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
        World world;

        public void Initilize(World world)
        {
            this.world = world;
            world.player.SetPosition(spawnPosition);
        }

        public void MovePlayer(LevelPosition distance)
        {
            LevelPosition newPos = new LevelPosition(
                world.player.position.x + distance.x,
                world.player.position.y + distance.y
            );

            if (0 > newPos.x || width-1 < newPos.x)
                return;
            if (0 > newPos.y || height-1 < newPos.y)
                return;


            if (objects.ContainsKey(newPos.ToString()))
            {
                Actor interact = world.actors[objects[newPos.ToString()]];
                if (interact != null)
                    interact.encounter.Run();
            } else 
            {
                world.player.SetPosition(newPos);
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

                    if ($"{world.player.position.x},{world.player.position.y}" == coord)
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