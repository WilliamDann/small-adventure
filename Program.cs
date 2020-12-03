using System;
using System.IO;
using System.Collections.Generic;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adventure
{
    class Program
    {
        static string LoadFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            } catch (IOException)
            {
                return $"Failed to load file {path}. Please reload the program.";
            }
        }

        static void Main(string[] args)
        {
            World world = LoadWorld();

            string introScreen    = LoadFile("screens/intro.txt");
            string controlsScreen = LoadFile("screens/controls.txt");
            string keysScreen     = LoadFile("screens/key.txt");

            Console.Clear();
            Console.WriteLine("What is your name?");
            world.Player.Name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine(introScreen);

            Console.WriteLine("Press any key to start playing...");
            Console.ReadKey();

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
                    case "reload":
                        world = LoadWorld();
                        Console.WriteLine("World Reloaded");
                        Console.ReadKey();
                        break;
                    case "help":
                        Console.Clear();
                        Console.WriteLine(controlsScreen);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "key":
                        Console.Clear();
                        Console.WriteLine(keysScreen);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }

                world.OnTurn();
            }
        }

        static World LoadWorld()
        {
            var items   = JsonSerializer.Deserialize<Dictionary<string, Item>>(
                File.ReadAllText("data/items.json"),
                new JsonSerializerOptions { }
            );

            var actors = JsonSerializer.Deserialize<Dictionary<string, Actor>>(
                File.ReadAllText("data/actors.json"),
                new JsonSerializerOptions 
                {
                    Converters = 
                    {
                        new ReferenceConverter<Item>(items)
                    }
                }
            );

            var levels = JsonSerializer.Deserialize<Dictionary<string, Level>>(
                File.ReadAllText("data/levels.json"),
                new JsonSerializerOptions 
                {
                    Converters = 
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
                }
            );


            World world = JsonSerializer.Deserialize<World>(
                File.ReadAllText("data/world.json"),
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new ReferenceConverter<Item>(items),
                        new ReferenceConverter<Actor>(actors),
                        new ReferenceConverter<Level>(levels),
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
                }
                );
            world.Items = items;
            world.Actors = actors;
            world.Levels = levels;

            return world;
        }
    }
}
