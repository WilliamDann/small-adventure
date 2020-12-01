using System.Collections.Generic;
using System;
namespace Adventure
{
    public class Level
    {
        // JSON populated data
        public string   Name { get; set; }
        public string[] Map  { get; set; }

        // because System.Text.Json does not support enums in 3.x
        //  int is being used for direction -_____-
        // N=0, E=1, S=2, W=3
        public Dictionary<string, int> SurroundingLevels { get; set; }

        public Position SpawnPosition { get; set; }


        public bool PositionIsOnMap(Position position)
        {
            if (0 > position.Y || Map.Length-1 < position.Y)
                return false;
            if (0 > position.X || Map[position.Y].Length-1 < position.X)
                return false;

            return true;
        }

        public int? FindDirectionOutOfMap(Position position)
        {
            if (position.Y < 0)
                return 0;
            if (position.Y >= Map.Length) 
                return 2;
            if (position.X >= Map[position.Y].Length) 
                return 1;
            if (position.X < 0)
                return 3;

            return null;
        }

        public string GetLevelInDirection(int direction)
        {
            foreach (string level in SurroundingLevels.Keys)
            {
                if (SurroundingLevels[level] == direction) return level;
            }
            return null;
        }

        public char GetCharAt(Position position)
        {
            return Map[position.Y][position.X];
        }

        public void SetCharAt(char character, Position position)
        {
            for (int y = 0; y < Map.Length; y++)
            {
                if (y == position.Y)
                {
                    char[] row = Map[y].ToCharArray();
                    row[position.X] = character;
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
}