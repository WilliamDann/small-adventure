using System;
using System.Collections.Generic;

using static System.Console;

namespace Adventure
{
    public class Actions
    {
        public static T GetListItem<T>(List<T> list)
        {
            if (list.Count == 0) throw new InvalidOperationException("cannot choose from an empty list");
            
            try
            {
                return list[int.Parse(ReadLine())];
            }
            catch (FormatException)
            {
                WriteLine("Input invalid, please enter an integer!");
                return GetListItem<T>(list);
            }
            catch (ArgumentOutOfRangeException)
            {
                WriteLine("Please enter a value in the list!");
                return GetListItem<T>(list);
            }
        }

        public static bool Confirm(string prompt)
        {
            Write($"{prompt} (y/n): ");
            string response = ReadLine().ToLower();

            if (response == "y")
                return true;
            else if (response == "n")
                return false;
            else
            {
                WriteLine("Please enter either 'y' or 'n'");
                return Confirm(prompt);
            }

        }

        public static void ListItems(List<Item> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                WriteLine($"{i}: {list[i]}");
            }
        }

        public static void Fight(Actor player, Actor other)
        {
            do
            {
                Console.Clear();
                WriteLine($"{player}\n\nvs.\n\n{other} ");

                player.Attack(other);
                if (other.Hp <= 0)
                {
                    WriteLine($"{other.Name} has died.");
                    WriteLine("Loot gained: ");
                    ListItems(other.Inventory);
                    foreach (Item item in other.Inventory)
                        player.Inventory.Add(item);

                    break;
                }


                other.Attack(player);
                if (player.Hp <= 0)
                {
                    foreach (Item item in player.Inventory)
                        other.Inventory.Add(item);
                    WriteLine($"{player.Name} has died.");

                    break;
                }
            } while (Confirm("Keep finghting?"));
        }

        public static void Trade(Actor player, Actor other)
        {
            if (!other.Tradable)
            {
                WriteLine($"{other.Name} will not trade with you");
                return;
            }

            List<Item> given = new List<Item>();
            List<Item> taken = new List<Item>();

            bool trading = true;
            do
            {
                Console.Clear();
                WriteLine("Items Given:");
                ListItems(given);
                WriteLine("---");

                WriteLine("Items Taken:");
                ListItems(taken);
                WriteLine("---");

                WriteLine("Your items:");
                ListItems(player.Inventory);
                WriteLine("---");

                WriteLine($"{other.Name}'s items:");
                ListItems(other.Inventory);
                WriteLine("---");

                WriteLine("1. Take an Item\n2. Give an item\n3.Finish Trade\n4.Quit");
                Write("Enter an option: ");

                switch (ReadLine())
                {
                    case "1":
                        if (other.Inventory.Count == 0)
                        {
                            WriteLine($"{other.Name}'s Inventory is empty");
                            WriteLine("Press any key to continue...");
                            ReadKey();
                            continue;
                        }
                        Write($"Enter Number from {other.Name}'s Inventory: ");

                        taken.Add(GetListItem<Item>(other.Inventory));
                        break;
                    case "2":
                        if (player.Inventory.Count == 0)
                        {
                            WriteLine("Your Inventory is empty");
                            WriteLine("Press any key to continue...");
                            ReadKey();
                            continue;
                        }
                        Write("Enter Number from Your Inventory: ");
                        
                        given.Add(GetListItem<Item>(player.Inventory));
                        break;
                    case "3":
                        int value = 0;
                        foreach(Item item in taken)
                            value -= item.Value;
                        foreach(Item item in given)
                            value += item.Value;

                        if (value >= 0)
                        {
                            WriteLine("It's a deal!");
                            foreach (Item item in given)
                            {
                                player.Inventory.Remove(item);
                                other.Inventory.Add(item);
                            }
                            foreach (Item item in taken)
                            {
                                player.Inventory.Add(item);
                                other.Inventory.Remove(item);
                            }
                            given.Clear();
                            taken.Clear();
                            
                            WriteLine("Press any key to continue...");
                            ReadKey();
                        }
                        else
                        {
                            WriteLine($"That is a bad trade for me. (value: {value})");
                            
                            WriteLine("Press any key to continue...");
                            ReadKey();
                        }
                        break;
                    case "4":
                        return;
                    default:
                        break;
                }
            } while (trading);
        }
    
        public static void ManageInventory(Actor player)
        {
            Console.Clear();
            Console.WriteLine(player);

            if (player.Inventory.Count == 0)
            {
                WriteLine("You inventory is empty.");
                return;
            }

            ListItems(player.Inventory);
            Write("Choose an item: ");
            Item item = GetListItem<Item>(player.Inventory);
            
            WriteLine("1. Equip Item\n2. Use Item\n3. Drop Item\n4. Quit");
            switch (ReadLine())
            {
                case "1":
                    if (item.Attack > 0)
                    {
                        player.Inventory.Remove(item);
                        player.Inventory.Add(player.Weapon);

                        player.Weapon = item;
                    }
                    else if (item.Defence > 0)
                    {
                        player.Inventory.Remove(item);
                        player.Inventory.Add(player.Armor);

                        player.Armor = item;
                    } else
                    {
                        WriteLine($"{item.Name} is not a weapon or armor");
                    }

                    break;
                case "2":
                    if (item.Effect == null)
                    {
                        WriteLine("This item has no effects");
                    } else 
                    {
                        item.Effect.Apply(player);
                        WriteLine($"Used {item}");
                    }

                    break;
                case "3":
                    player.Inventory.Remove(item);
                    WriteLine("Item Dropped");

                    break;
                case "4":
                    return;
                default:
                    WriteLine("Please enter a number from 1-4");
                    
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();

                    ManageInventory(player);
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}