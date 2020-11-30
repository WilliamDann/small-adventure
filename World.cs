using System.Collections.Generic;

namespace Adventure
{
    
    public class World
    {
        public Player player { get; set; }

        public Dictionary<string, Level> levels { get; set; }
        public Level CurrentLevel { get; private set;}

        public void LoadLevel(string level)
        {
            if (!levels.ContainsKey(level))
                throw new KeyNotFoundException($"Level {level} does not exist");
        
            CurrentLevel = levels[level];
            player.SetPosition(CurrentLevel.spawnPosition);
        }

        public void MovePlayer(LevelPosition distance)
        {
            LevelPosition newPos = new LevelPosition(
                player.position.x + distance.x,
                player.position.y + distance.y
            );

            if (!CurrentLevel.PositionIsOnMap(newPos))
            {
                bool up   = newPos.y < 0;
                bool right = newPos.x < 0;
                bool down = newPos.y >  CurrentLevel.map.Length;
                bool left = newPos.x >= CurrentLevel.map[newPos.y].Length;

                if (up && CurrentLevel.surroundingLevels[0] != null)
                    LoadLevel(CurrentLevel.surroundingLevels[0]);
                else if (right && CurrentLevel.surroundingLevels[1] != null)
                    LoadLevel(CurrentLevel.surroundingLevels[1]);
                else if (down && CurrentLevel.surroundingLevels[2] != null)
                    LoadLevel(CurrentLevel.surroundingLevels[2]);
                else if (left && CurrentLevel.surroundingLevels[3] != null)
                    LoadLevel(CurrentLevel.surroundingLevels[3]);

                return;
            }
            
            string ground = CurrentLevel.GetCharAt(newPos);
            if (ground == "_" || ground == "=")
            {
                player.SetPosition(newPos);
            }
            else if (CurrentLevel.actors.ContainsKey(ground))
            {
                Actor interact = CurrentLevel.actors[ground];
                if (interact.Encounter != null)
                    interact.Encounter.Run(context: this);
            }
        }
 
        public string GetMapDisplay()
        {
            char temp;
            string display;

            temp = CurrentLevel.GetCharAt(player.position).ToCharArray()[0];
            CurrentLevel.SetCharAt(player.Character.ToCharArray()[0], player.position);

            display = CurrentLevel.ToString();
            CurrentLevel.SetCharAt(temp, player.position);

            return display;
        }
    }
}