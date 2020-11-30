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
            FileEncounter introMenu   = new FileEncounter("screens/intro.txt");
            FileEncounter controlMenu = new FileEncounter("screens/controls.txt");
            FileEncounter keyMenu     = new FileEncounter("screens/key.txt");
            introMenu.Run();

            Console.Clear();
            Console.WriteLine("What is your name?");
            world.Player.Name = Console.ReadLine();

            world.LoadLevel("The Crossroads");

            bool done = false;
            while (!done)
            {
                Console.Clear();
                Console.WriteLine(world.CurrentLevel.Name);
                Console.WriteLine(world.GetMapDisplay());
                Console.WriteLine(world.Player);

                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    // movment
                    case "up":
                    case "u":
                        world.MovePlayer(new LevelPosition(0, -1));
                        break;
                    case "down":
                    case "d":
                        world.MovePlayer(new LevelPosition(0, 1));
                        break;
                    case "left":
                    case "l":
                        world.MovePlayer(new LevelPosition(-1, 0));
                        break;
                    case "right":
                    case "r":
                        world.MovePlayer(new LevelPosition(1, 0));
                        break;

                    // menus
                    case "quit":
                        done = true;
                        break;
                    case "help":
                        controlMenu.Run();
                        break;
                    case "key":
                        keyMenu.Run();
                        break;
                }
            }
        }
    }
}
