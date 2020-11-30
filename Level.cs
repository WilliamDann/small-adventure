using System.Collections.Generic;
using System;
namespace Adventure
{
    public class Level
    {
        // JSON populated data
        public string name  { get; set; }
        public string[] map { get; set; }

        // 0=N, 1=E, 2=S, 3=W
        public string[] surroundingLevels { get; set; }

        public LevelPosition spawnPosition { get; set; }

        public Dictionary<string, Actor> actors { get; set; }

        // local data
        World world;

        public Level()
        {
            this.actors = new Dictionary<string, Actor>();
        }

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
            if (!PositionOnMap(newPos))
            {
                // south
                if (newPos.y >= map.Length)
                {
                    if (surroundingLevels[2] != null)
                        world.LoadLevel(surroundingLevels[2]);
                }

                // north
                else if (newPos.y < 0)
                {
                    if (surroundingLevels[0] != null)
                        world.LoadLevel(surroundingLevels[0]);
                }

                // west
                else if (newPos.x < 0)
                {
                    if (surroundingLevels[3] != null)
                        world.LoadLevel(surroundingLevels[3]);
                }

                // east
                else if (newPos.x >= map[newPos.y].Length)
                {
                    if (surroundingLevels[1] != null)
                        world.LoadLevel(surroundingLevels[1]);
                }

                return;
            }

            string ground = GetCharAt(newPos);
            if (ground == "_" || ground == "=")
            {
                world.player.SetPosition(newPos);
            }
            else if (actors.ContainsKey(ground))
            {
                Actor interact = actors[ground];
                if (interact.encounter != null)
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