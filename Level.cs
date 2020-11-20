using System.Collections.Generic;
using System;
namespace Adventure
{
    public class Level
    {
        // JSON populated data
        public string name  { get; set; }
        public string[] map { get; set; }

        public LevelPosition spawnPosition { get; set; }

        // local data
        World world;

        public void Initilize(World world)
        {
            this.world = world;
            world.player.SetPosition(spawnPosition);
        }

        public bool PositionOnMap(LevelPosition pos)
        {
            if (0 > pos.y || map.Length-1 < pos.y)
                return false;
            if (0 > pos.x || map[pos.y].Length-1 < pos.x)
                return false;

            return true;
        }

        public string GetCharAt(LevelPosition pos)
        {
            if (!PositionOnMap(pos)) return null;

            return map[pos.y].ToCharArray()[pos.x].ToString();
        }

        public void SetCharAt(char character, LevelPosition pos)
        {
            if (!PositionOnMap(pos)) return;

            for (int y = 0; y < map.Length; y++)
            {
                if (y == world.player.position.y)
                {
                    char[] row = map[y].ToCharArray();
                    row[world.player.position.x] = character;
                }
            }
        }

        public void MovePlayer(LevelPosition distance)
        {
            LevelPosition newPos = new LevelPosition(
                world.player.position.x + distance.x,
                world.player.position.y + distance.y
            );
            string ground = GetCharAt(newPos);
            
            if (ground == null) return;
            
            if (ground == "_")
            {
                world.player.SetPosition(newPos);
            }
            else if (world.actors.ContainsKey(ground))
            {
                Actor interact = world.actors[ground];
                if (interact != null)
                    interact.encounter.Run();
            }
        }

        public override string ToString()
        {
            string display = "";

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (world.player.position.y == y && world.player.position.x == x)
                        display += $" {world.player.character} ";
                    else 
                        display += $" {map[y][x]} ";
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