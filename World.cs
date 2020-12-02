using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adventure
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public class World
    {
        public Dictionary<string, Level> Levels { get; set; }

        public Player Player { get; set; }

        public Dictionary<string, Item> Items   { get; set; }
        public Dictionary<string, Actor> Actors { get; set; }

        public List<char> WalkableTiles { get; set; }

        public void MoveActor(Actor actor, Position distance)
        {
            WorldPosition newPos = new WorldPosition
            (
                actor.Position.X + distance.X,
                actor.Position.Y + distance.Y,
                actor.Position.Level
            );

            if (Levels[newPos.Level].PositionIsOnMap(newPos))
            {
                char tile = Levels[newPos.Level].GetCharAt(newPos);

                if (WalkableTiles.Contains(tile))
                    actor.Position = newPos;
            }
            else
            {
                Direction outDirection = (Direction)Levels[newPos.Level].FindDirectionOutOfMap(new Position(newPos.X, newPos.Y));

                string newLevel = Levels[newPos.Level].GetLevelNameInDirection(outDirection);
                if (newLevel == null)
                {
                    return;
                }

                newPos = new WorldPosition(
                    FindNewLevelPosition(newPos, Levels[newLevel], outDirection),
                    newLevel
                );

                actor.Position = newPos;
            }
        }

        public void SetCurrentLevel(string level)
        {
            if (!Levels.ContainsKey(level))
                throw new KeyNotFoundException($"Level {level} does not exist");

            Player.Position = new WorldPosition(Levels[level].SpawnPosition, level);
        }

        public Level GetCurrentLevel()
        {
            return Levels[Player.Position.Level];
        }
 
        public List<Actor> GetActorsInLevel(string level)
        {
            List<Actor> actors = new List<Actor>();
            foreach (string key in Actors.Keys)
                if (Actors[key].Position.Level == level)
                    actors.Add(Actors[key]);
            return actors;
        }

        public string GetLevelMap(string levelName=null)
        {
            if (levelName == null)
                levelName = Player.Position.Level;
            string display = "";

            string[] map = CopyMap(Levels[Player.Position.Level]);
            foreach (Actor actor in GetActorsInLevel(levelName))
            {
                map = PlaceOnMap(map, actor.Character[0], actor.Position);
            }
            map = PlaceOnMap(map, Player.Character[0], Player.Position);

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

        /// helpers
        string[] CopyMap(Level level)
        {
            int rows = level.Map.Length; 
            string[] map = new string[rows];
            for (int i = 0; i < rows; i++)
            {
                map[i] = level.Map[i];
            }
            return map;
        }

        string[] PlaceOnMap(string[] map, char c, Position pos)
        {
            char[] row = map[pos.Y].ToCharArray();
            row[pos.X] = c;
            map[pos.Y] = new string(row);
            return map;
        }

        Position FindNewLevelPosition(Position currentPos, Level to, Direction direction)
        {
            Position position = new Position(currentPos.X, currentPos.Y);
            if (position.Y >= to.Map.Length)
                position.Y = to.Map.Length-1;
            if (position.Y < 0)
                position.Y = 0;

            if (position.X >= to.Map[position.Y].Length)
                position.X = to.Map[position.Y].Length-1;

            if (direction == Direction.North)
                position.Y = to.Map.Length-1;
            else if (direction == Direction.South)
                position.Y = 0;
            else if (direction == Direction.East)
                position.X = 0;
            else if (direction == Direction.West)
                position.X = to.Map[position.Y].Length-1;
            
            return position;
        }
    }

    public class Position
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public Position() { }
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }

    public class WorldPosition: Position
    {
        public string Level { get; set; }

        public WorldPosition() { }
        public WorldPosition(int x, int y, string level)
        {
            this.X = x;
            this.Y = y;
            this.Level = level;
        }
        public WorldPosition(Position position, string level)
        {
            this.X = position.X;
            this.Y = position.Y;
            this.Level = level;
        }

        public override string ToString()
        {
            return $"{X},{Y} on {Level}";
        }
    }
}