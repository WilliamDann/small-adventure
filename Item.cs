using System;

namespace Adventure
{
    public class ItemEffect
    {
        public int Heal { get; set; } = 0;
        public int Hurt { get; set; } = 0;

        public void Apply(Actor actor)
        {
            actor.AdjustHp(Heal);
            actor.AdjustHp(-Hurt);
        }

        public override string ToString()
        {
            string str = "(";

            if (Heal != 0) str += $" Heal: {Heal} ";
            if (Hurt != 0) str += $" Hurt: {Hurt} ";

            return str + ")";
        }
    }

    public class Item : IReferable
    {
        public string Name { get; set; }

        public int Defence      { get; set; } = 0;
        public int Attack       { get; set; } = 0;

        public int Value { get; set; } = 0;

        public ItemEffect Effect { get; set; }

        public override string ToString()
        {
            string display = $"{Name}(";

            if (Defence != 0)
                display += $" Defence: {Defence} ";
            if (Attack != 0)
                display += $" Attack: {Attack} ";
            if (Value != 0)
                display += $" Value: {Value} gold ";
            if (Effect != null)
                display += $" Effect: {Effect} ";

            display += ")";
            return display;
        }
    }
}