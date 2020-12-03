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

        public string Message  { get; set; }
        public bool   Tradable { get; set; } = true;

        public virtual void Interact(Actor other)
        {
            BuildBaseMenu().Run(this, other);
        }

        public void Move(World world, Position distance)
        {
                WorldPosition newPos = new WorldPosition
            (
                Position.X + distance.X,
                Position.Y + distance.Y,
                Position.Level
            );

            if (world.Levels[newPos.Level].PositionIsOnMap(newPos))
            {
                char tile = world.Levels[newPos.Level].GetCharAt(newPos);

                if (!world.WalkableTiles.Contains(tile))
                    return;

                Actor interact = world.GetActorAtPosition(newPos);
                if (interact != null)
                {
                    Interact(interact);
                    return;
                }

                Position = newPos;
            }
            else
            {
                Direction? outDirection = world.Levels[newPos.Level].FindDirectionOutOfMap(new Position(newPos.X, newPos.Y));
                string newLevel         = world.Levels[newPos.Level].GetLevelNameInDirection((Direction)outDirection);

                if (newLevel == null)
                {
                    return;
                }

                newPos = new WorldPosition(
                    world.FindNewLevelPosition(newPos, world.Levels[newLevel], (Direction)outDirection),
                    newLevel
                );

                Position = newPos;
            }
        }

        public void Attack(Actor other, bool output=true)
        {
            int diff =  -Weapon.Attack + other.Armor.Defence;
            if (diff > 0) diff = 0;

            if (output)
            {
                Console.WriteLine($"{Name} attacked {other.Name} with {-diff} damage ({other.Armor.Defence} was defended)");
            }

            other.Hp += diff;
        }

        public void AdjustHp(int amount)
        {
            Hp += amount;

            if (Hp >= MaxHp)
                Hp = MaxHp;
        }

        protected TextMenu BuildBaseMenu()
        {
            return new TextMenu(
                null,
                (self, other) => 
                {
                    Console.WriteLine(other.Message);
                },
                new List<TextMenu>
                {
                    new TextMenu("Fight", (self, other) => { Actions.Fight(self, other); }),
                    new TextMenu("Trade", (self, other) => { Actions.Trade(self, other); }),
                    new TextMenu("Leave", (self, other) => { Console.WriteLine("You left."); })
                }
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