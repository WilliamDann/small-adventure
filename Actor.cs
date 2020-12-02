using System.Collections.Generic;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adventure
{
    public class Actor : IReferable
    {
        public WorldPosition Position  { get; set; }
        public string        Character { get; set; }

        public int Hp           { get; set; } = 1;
        public int MaxHp        { get; set; } = 1;
        public string Name      { get; set; }

        public List<Item> Inventory { get; set; }
        public Item Weapon          { get; set; }
        public Item Armor           { get; set; }

        public string EncounterMessage { get; set; }

        public void Interact(Actor other)
        {
            TextMenu baseMenu = new TextMenu(
                EncounterMessage,
                (self, other) => 
                {

                },
                null
            );
        }

        public override string ToString()
        {
            string display = "";
            display += $"{Name} ({Character})\n";
            display += $"Hp: {Hp}/{MaxHp}\n";
            display += $"Weapon: {Weapon}\n";
            display += $"Armor: {Armor}\n";

            display += "Inventory: \n";
            foreach(Item item in Inventory)
            {
                display += $"\t{item}\n";
            }
            return display;
        }
    }

    public class Player : Actor
    {

    }
}   