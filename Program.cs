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

            Level level = world.levels["The River"];
            level.Initilize(world);

            bool done = false;
            while (!done)
            {
                Console.Clear();
                Console.WriteLine(level.name);
                Console.WriteLine(level);

                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    // movment
                    case "up":
                    case "u":
                        level.MovePlayer(new LevelPosition(0, -1));
                        break;
                    case "down":
                    case "d":
                        level.MovePlayer(new LevelPosition(0, 1));
                        break;
                    case "left":
                    case "l":
                        level.MovePlayer(new LevelPosition(-1, 0));
                        break;
                    case "right":
                    case "r":
                        level.MovePlayer(new LevelPosition(1, 0));
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
