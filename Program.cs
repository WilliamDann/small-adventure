using System;
using System.IO;
using System.Collections.Generic;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            World world = LoadWorld();

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
                    case "reload":
                        world = LoadWorld();
                        Console.WriteLine("World Reloaded");
                        Console.ReadKey();
                        break;
                    case "help":
                        // controlMenu.Run();
                        break;
                    case "key":
                        // keyMenu.Run();
                        break;
                }

                world.RemoveDead();
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
