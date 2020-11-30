using System.Collections.Generic;
using System;
using System.Text.Json;

namespace Adventure
{
    public class Actor
    {
        public int Hp           { get; set; }
        public int MaxHp        { get; set; }
        public string Name      { get; set; }

        public int Defence      { get; set; } = 0;

        public string Character { get; set; }

        public List<Item> Inventory { get; set; }
        public Item Weapon          { get; set; }
        public Item Armor           { get; set; }

        public Encounter Encounter { get; set; }

        public void ModifyHp(int amount)
        {
            this.Hp += amount;
            if (Hp > MaxHp)
                Hp = MaxHp;
        }

        public void ModifyMaxHp(int amount)
        {
            this.MaxHp += amount;
            if (MaxHp < 0) MaxHp = 0;
        }

        public void ModifyDefence(int amount)
        {
            this.Defence += amount;
            if (Defence < 0) Defence = 0;
        }

        public override string ToString()
        {
            string display = "----------\n";

            display += $"{Name} ({Character})\n";
            display += $"Hp: {Hp}/{MaxHp}\n";
            display += $"Weapon: {Weapon}\n";
            display += $"Armor: {Armor}\n";

            display += "Inventory: \n";
            foreach(Item item in Inventory)
            {
                display += $"\t{item}\n";
            }
            
            display += "----------";
            return display;
        }

    }
}