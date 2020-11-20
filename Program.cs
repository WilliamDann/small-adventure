using System;
using System.IO;
using System.Text.Json;

namespace Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            bool done = false;

            Level crossroads = JsonSerializer.Deserialize<Level>(File.ReadAllText("data/levels/crossroads.json"));
            while (!done)
            {
                Console.Clear();
                Console.WriteLine(crossroads.name);
                Console.WriteLine(crossroads);

                string input = Console.ReadLine();
                if (input.ToLower() == "quit")
                {
                    break;
                }
                if (input.ToLower() == "help")
                {
                    Console.Clear();
                    Console.WriteLine(File.ReadAllText("controls.txt"));
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}
