using System;
using System.IO;
using System.Collections.Generic;
using static System.Console;

namespace Adventure
{
    public class Encounter 
    {
        public string Message          { get; set; }
        public string OptionName       { get; set; }
        public Effect Effect           { get; set; }

        public List<Encounter> Options { get; set; }

        public Encounter()
        {
            this.Options = new List<Encounter>();
        }

        void ListOptions()
        {
            int num = 0;
            foreach (Encounter option in Options)
            {
                WriteLine($"{num}: {option.OptionName}");
                num++;
            }
        }

        int? ChooseOption()
        {
           WriteLine("Respond with: ");

            int? choice = null;
            try
            {
                choice = int.Parse(ReadLine());
            } catch (FormatException)
            {
                WriteLine("Huh? I dont understand that");
            }

            if (choice > Options.Count)
                choice = null;

            return choice;
        }

        void PauseConsole()
        {
            WriteLine("Press any key to continue...");
            ReadKey();
        }

        public virtual void Run(World context)
        {
            Clear();
            WriteLine(Message);
            if (Effect != null)
                Effect.Apply(context);

            if (Options.Count == 0)
            {
                PauseConsole();
                return;
            }

            int? option = null;
            while (option == null)
            {
                Console.Clear();
                ListOptions();
                option = ChooseOption();
            }

            Options[(int)option].Run(context);
        }
    }

    public class FileEncounter : Encounter
    {
        public FileEncounter(string path)
        {
            try
            {
                Message  = File.ReadAllText(path);
            } catch (IOException)
            {
                Message = "File Encounter load failed!";
            }
        }

        public override void Run(World context = null)
        {
            base.Run(context);
        }
    }
}