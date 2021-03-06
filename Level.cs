using System.Collections.Generic;
using System;
namespace Adventure
{
    public class Level: IReferable
    {
        // JSON populated data
        public string   Name { get; set; }
        public string[] Map  { get; set; }

        public Dictionary<string, Direction> SurroundingLevels { get; set; }

        public bool PositionIsOnMap(Position position)
        {
            if (0 > position.Y || Map.Length-1 < position.Y)
                return false;
            if (0 > position.X || Map[position.Y].Length-1 < position.X)
                return false;

            return true;
        }

        public Direction? FindDirectionOutOfMap(Position position)
        {
            if (position.Y < 0)
                return Direction.North;
            if (position.Y >= Map.Length) 
                return Direction.South;
            if (position.X >= Map[position.Y].Length) 
                return Direction.East;
            if (position.X < 0)
                return Direction.West;

            return null;
        }

        public string GetLevelNameInDirection(Direction direction)
        {
            foreach (string level in SurroundingLevels.Keys)
                if (SurroundingLevels[level] == direction)
                    return level;
            return null;
        }

        public char GetCharAt(Position position)
        {
            return Map[position.Y][position.X];
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