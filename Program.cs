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
            TextFileEvent introMenu   = new TextFileEvent("screens/intro.txt");
            TextFileEvent controlMenu = new TextFileEvent("screens/controls.txt");
            TextFileEvent keyMenu     = new TextFileEvent("screens/key.txt");
            introMenu.Run();

            Console.Clear();
            Console.WriteLine("What is your name?");
            world.player.name = Console.ReadLine();

            world.LoadLevel("The Crossroads");

            bool done = false;
            while (!done)
            {
                Console.Clear();
                Console.WriteLine(world.CurrentLevel.name);
                Console.WriteLine(world.CurrentLevel);
                Console.WriteLine(world.player);

                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    // movment
                    case "up":
                    case "u":
                        world.CurrentLevel.MovePlayer(new LevelPosition(0, -1));
                        break;
                    case "down":
                    case "d":
                        world.CurrentLevel.MovePlayer(new LevelPosition(0, 1));
                        break;
                    case "left":
                    case "l":
                        world.CurrentLevel.MovePlayer(new LevelPosition(-1, 0));
                        break;
                    case "right":
                    case "r":
                        world.CurrentLevel.MovePlayer(new LevelPosition(1, 0));
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
