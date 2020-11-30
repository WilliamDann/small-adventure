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

        public Level()
        {
            this.actors = new Dictionary<string, Actor>();
        }

        public bool PositionIsOnMap(LevelPosition pos)
        {
            if (0 > pos.y || map.Length-1 < pos.y)
                return false;
            if (0 > pos.x || map[pos.y].Length-1 < pos.x)
                return false;

            return true;
        }

        public string GetCharAt(LevelPosition pos)
        {
            if (!PositionIsOnMap(pos)) return null;

            return map[pos.y].ToCharArray()[pos.x].ToString();
        }

        public void SetCharAt(char character, LevelPosition pos)
        {
            if (!PositionIsOnMap(pos)) return;

            for (int y = 0; y < map.Length; y++)
            {
                if (y == pos.y)
                {
                    char[] row = map[y].ToCharArray();
                    row[pos.x] = character;
                    map[y] = new string(row);
                }
            }
        }

        public override string ToString()
        {
            string display = "";

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
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