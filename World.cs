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

        public Actor GetActorAtPosition(WorldPosition position)
        {
            foreach (string key in Actors.Keys)
                if (position.X == Actors[key].Position.X && position.Y == Actors[key].Position.Y && position.Level == Actors[key].Position.Level)
                    return Actors[key];
            return null;
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

        // ran every turn of the game
        public void OnTurn()
        {
            foreach (Item item in Actors["Town Guard"].Inventory)
                if (item.Name == "Silver Ring")
                {
                    Actors["Town Guard"].Message = "Thanks for finding my ring! You can enter the town now";
                    Actors["Town Guard"].Move(this, new Position(1, 0));
                }

            if (Actors["Water Well"].Inventory.Count == 0)
            {
                Actors["Water Well"].Inventory.Add(Items["Water"]);
            }

            RemoveDead();
        }

        public void RemoveDead()
        {
            foreach (string key in Actors.Keys)
                if (Actors[key].Hp <=0 )
                    Actors.Remove(key);
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

        public Position FindNewLevelPosition(Position currentPos, Level to, Direction direction)
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