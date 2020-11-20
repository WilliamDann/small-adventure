using System;

namespace Adventure
{
    public class Player : Actor
    {
        public LevelPosition position { get; private set; }

        public void SetPosition(LevelPosition position)
        {
            this.position = position;
        }
    }
}