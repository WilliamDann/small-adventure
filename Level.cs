using System.Collections.Generic;
using System;
namespace Adventure
{
    public class Level
    {
        // JSON populated data
        public string   Name { get; set; }
        public string[] Map  { get; set; }

        // 0=N, 1=E, 2=S, 3=W
        public string[] SurroundingLevels { get; set; }

        public LevelPosition SpawnPosition { get; set; }

        public Dictionary<string, Actor> Actors { get; set; }

        public Level()
        {
            this.Actors = new Dictionary<string, Actor>();
        }

        public bool PositionIsOnMap(LevelPosition pos)
        {
            if (0 > pos.Y || Map.Length-1 < pos.Y)
                return false;
            if (0 > pos.X || Map[pos.Y].Length-1 < pos.X)
                return false;

            return true;
        }

        public string GetCharAt(LevelPosition pos)
        {
            if (!PositionIsOnMap(pos)) return null;

            return Map[pos.Y].ToCharArray()[pos.X].ToString();
        }

        public void SetCharAt(char character, LevelPosition pos)
        {
            if (!PositionIsOnMap(pos)) return;

            for (int y = 0; y < Map.Length; y++)
            {
                if (y == pos.Y)
                {
                    char[] row = Map[y].ToCharArray();
                    row[pos.X] = character;
                    Map[y] = new string(row);
                }
            }
        }

        public override string ToString()
        {
            string display = "";

            for (int y = 0; y < Map.Length; y++)
            {
                for (int x = 0; x < Map[y].Length; x++)
                {
                    display += $" {Map[y][x]} ";
                }
                display += "\n";
            }

            return display;
        }
    }

    public class LevelPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public LevelPosition() { }
        public LevelPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}