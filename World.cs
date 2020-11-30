using System.Collections.Generic;

namespace Adventure
{
    
    public class World
    {
        public Player Player { get; set; }

        public Dictionary<string, Level> Levels { get; set; }
        public Level CurrentLevel { get; private set;}

        public void LoadLevel(string level)
        {
            if (!Levels.ContainsKey(level))
                throw new KeyNotFoundException($"Level {level} does not exist");
        
            CurrentLevel = Levels[level];
            Player.SetPosition(CurrentLevel.SpawnPosition);
        }

        public void MovePlayer(LevelPosition distance)
        {
            LevelPosition newPos = new LevelPosition(
                Player.position.X + distance.X,
                Player.position.Y + distance.Y
            );

            if (!CurrentLevel.PositionIsOnMap(newPos))
            {
                bool up    = newPos.Y < 0;
                bool left = newPos.X < 0;
                bool down  = newPos.Y >= CurrentLevel.Map.Length;

                bool right  = false;
                if (!up && !down)
                    right = newPos.X >= CurrentLevel.Map[newPos.Y].Length;

                if (up && CurrentLevel.SurroundingLevels[0] != null)
                    LoadLevel(CurrentLevel.SurroundingLevels[0]);
                else if (right && CurrentLevel.SurroundingLevels[1] != null)
                    LoadLevel(CurrentLevel.SurroundingLevels[1]);
                else if (down && CurrentLevel.SurroundingLevels[2] != null)
                    LoadLevel(CurrentLevel.SurroundingLevels[2]);
                else if (left && CurrentLevel.SurroundingLevels[3] != null)
                    LoadLevel(CurrentLevel.SurroundingLevels[3]);

                return;
            }
            
            string ground = CurrentLevel.GetCharAt(newPos);
            if (ground == "_" || ground == "=")
            {
                Player.SetPosition(newPos);
            }
            else if (CurrentLevel.Actors.ContainsKey(ground))
            {
                Actor interact = CurrentLevel.Actors[ground];
                if (interact.Encounter != null)
                    interact.Encounter.Run(context: this);
            }
        }
 
        public string GetMapDisplay()
        {
            char temp;
            string display;

            temp = CurrentLevel.GetCharAt(Player.position).ToCharArray()[0];
            CurrentLevel.SetCharAt(Player.Character.ToCharArray()[0], Player.position);

            display = CurrentLevel.ToString();
            CurrentLevel.SetCharAt(temp, Player.position);

            return display;
        }
    }
}