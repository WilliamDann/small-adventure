using System;
using System.IO;
using System.Text.Json;

namespace Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            World world = JsonSerializer.Deserialize<World>(File.ReadAllText("data/world.json"));
            // FileEncounter controlMenu = new FileEncounter("screens/controls.txt");
            // FileEncounter keyMenu     = new FileEncounter("screens/key.txt");

            Console.Clear();
            Console.WriteLine("What is your name?");
            world.Player.Name = Console.ReadLine();

            bool done = false;
            while (!done)
            {
                Console.Clear();
                Console.WriteLine(world.Player.Position.Level);
                Console.WriteLine(world.GetLevelMap());
                Console.WriteLine(world.Player);

                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    // movment
                    case "up":
                    case "u":
                        world.MoveActor(world.Player, new Position(0, -1));
                        break;
                    case "down":
                    case "d":
                        world.MoveActor(world.Player, new Position(0, 1));
                        break;
                    case "left":
                    case "l":
                        world.MoveActor(world.Player, new Position(-1, 0));
                        break;
                    case "right":
                    case "r":
                        world.MoveActor(world.Player, new Position(1, 0));
                        break;

                    // menus
                    case "quit":
                        done = true;
                        break;
                    case "help":
                        // controlMenu.Run();
                        break;
                    case "key":
                        // keyMenu.Run();
                        break;
                }
            }
        }
    }
}
