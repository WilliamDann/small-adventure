using System;
using System.IO;
using System.Text.Json;

namespace Adventure
{
    class Program
    {        
        static void Main(string[] args)
        {
            Player player = JsonSerializer.Deserialize<Player>(File.ReadAllText("data/player.json"));

            bool done = false;

            Console.Clear();
            Console.WriteLine(File.ReadAllText("screens/intro.txt"));
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Level crossroads = JsonSerializer.Deserialize<Level>(File.ReadAllText("data/levels/crossroads.json"));
            crossroads.Initilize(player);
            while (!done)
            {
                Console.Clear();
                Console.WriteLine(crossroads.name);
                Console.WriteLine(crossroads);

                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    // movment
                    case "up":
                    case "u":
                        crossroads.MovePlayer(new LevelPosition(0, -1));
                        break;
                    case "down":
                    case "d":
                        crossroads.MovePlayer(new LevelPosition(0, 1));
                        break;
                    case "left":
                    case "l":
                        crossroads.MovePlayer(new LevelPosition(-1, 0));
                        break;
                    case "right":
                    case "r":
                        crossroads.MovePlayer(new LevelPosition(1, 0));
                        break;

                    // menus
                    case "quit":
                        done = true;
                        break;
                    case "help":
                        Console.Clear();
                        Console.WriteLine(File.ReadAllText("screens/controls.txt"));
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "key":
                        Console.Clear();
                        Console.WriteLine(File.ReadAllText("screens/key.txt"));
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
