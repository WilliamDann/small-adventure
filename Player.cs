using System;

namespace Adventure
{
    public class Player
    {
        public LevelPosition position { get; private set; }

        public void SetPosition(LevelPosition position)
        {
            this.position = position;
        }
    }
}