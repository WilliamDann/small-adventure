using System;
using System.Collections.Generic;

using static System.Console;

namespace Adventure
{
    public class Actions
    {
        public static T GetListItem<T>(List<T> list)
        {
            try
            {
                return list[int.Parse(ReadLine())];
            }
            catch (FormatException)
            {
                WriteLine("Input invalid, please enter an integer");
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

                WriteLine("1. Add item to taken\n2. Add item to given\n3.Finish Trade\n4.Quit");
                Write("Enter an option: ");

                switch (ReadLine())
                {
                    case "1":
                        Write($"Enter Number from {other.Name}'s Inventory: ");

                        taken.Add(GetListItem<Item>(other.Inventory));
                        break;
                    case "2":
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
    }
}