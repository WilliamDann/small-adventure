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
            IMenu introMenu   = new TextFileMenu("screens/intro.txt");
            IMenu controlMenu = new TextFileMenu("screens/controls.txt");
            IMenu keyMenu     = new TextFileMenu("screens/key.txt");

            introMenu.ShowMenu();

            Level crossroads = JsonSerializer.Deserialize<Level>(File.ReadAllText("data/levels/crossroads.json"));
            crossroads.Initilize(player);
            
            bool done = false;
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
                        controlMenu.ShowMenu();
                        break;
                    case "key":
                        keyMenu.ShowMenu();
                        break;
                }
            }
        }
    }
}
