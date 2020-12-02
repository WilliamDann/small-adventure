using System;
using static System.Console;

namespace Adventure
{
    public class Actions
    {
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
        public static void Fight(Actor first, Actor second, bool output=true)
        {
            bool fighting = true;
            while (fighting)
            {
                Console.Clear();
                WriteLine($"{first}\n\nvs.\n\n{second} ");

                first.Attack(second);
                if (second.Hp <= 0)
                {
                    foreach (Item item in second.Inventory)
                        first.Inventory.Add(item);

                    if (output)
                        WriteLine($"{second.Name} has died.");

                    fighting = false;
                }

                if (fighting)
                {
                    second.Attack(first);
                    if (first.Hp <= 0)
                    {
                        foreach (Item item in first.Inventory)
                            second.Inventory.Add(item);
                        if (output)
                            WriteLine($"{first.Name} has died.");
                    
                        fighting = false;
                    }

                }

                if (fighting && output)
                {
                    fighting = Confirm("Keep fighting?");
                }
            }
        }
    }
}