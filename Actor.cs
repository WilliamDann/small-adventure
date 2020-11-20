using System.Collections.Generic;
using System;
using System.Text.Json;

namespace Adventure
{
    public class Actor
    {
        public int hp           { get; set; }
        public int maxHP        { get; set; }
        public string name      { get; set; }

        public string character { get; set; }

        public TextEncounter encounter { get; set; }
    }

    public class TextEncounter : Event
    {
        public Dictionary<string, TextEncounter> responses { get; set; }
        public string runnableString { get; set; }

        public TextEncounter() { }
        public TextEncounter(string message): base(message) { }

        public override void Run()
        {
            Console.Clear();
            Console.WriteLine(message);
            if (responses == null)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Dictionary<string, TextEncounter> responsesCopy = new Dictionary<string, TextEncounter>(responses);
            responsesCopy.Add("Say Goodbye", new TextEncounter("Good travels, adventuerer!"));

            List<string> numbered = new List<String>();
            foreach(string response in responsesCopy.Keys)
            {
                Console.WriteLine($"{numbered.Count}: {response}");
                numbered.Add(response);
            }
            Console.WriteLine("Enter the number of your response:");
            
            TextEncounter newEncounter = null;
            try
            {
                int choice = int.Parse(Console.ReadLine());
                newEncounter = responsesCopy[numbered[choice]];
            } catch (FormatException)
            {
                Console.WriteLine("Huh? I dont understand that");
                Console.ReadKey();
                return;
            } catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Huh? I dont understand that");
                Console.ReadKey();
                return;
            }

            if (newEncounter.message != null)
                newEncounter.Run();
            else
                Console.ReadKey();
        }
    }
}