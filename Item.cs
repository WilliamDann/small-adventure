using System;

namespace Adventure
{
    public class Item : IReferable
    {
        public string Name { get; set; }

        public int Defence      { get; set; }  = 0;
        public int Attack       { get; set; } = 0;

        public int Durability    { get; set; } = 100;
        public int DurabilityMax { get; set; } = 100;

        public int Value { get; set; } = 0;

        public override string ToString()
        {
            string display = $"{Name}(";

            if (Defence != 0)
                display += $" Defence: {Defence} ";
            if (Attack != 0)
                display += $" Attack: {Attack} ";
            if (Value != 0)
                display += $" Value: {Value} gold ";

            display += ")";
            return display;
        }
    }
}