using System;
using System.Collections.Generic;

namespace Adventure
{
    public class TextMenu
    {
        public string               OptionName { get; private set; }
        public Action<Actor, Actor> Effect     { get; private set; }
        public List<TextMenu>       Options    { get; private set; }


        public TextMenu(string OptionName) 
            : this(OptionName, (a, b) => {}, null) { }
        public TextMenu(string OptionName, Action<Actor, Actor> Effect)
            : this(OptionName, Effect, null) { }

        public TextMenu(string OptionName, Action<Actor, Actor> Effect, List<TextMenu> Options)
        {
            this.OptionName = OptionName;
            this.Effect  = Effect;
            this.Options = Options;

            if (Options == null)
                this.Options = new List<TextMenu>();
        }

        public TextMenu ChooseOption()
        {
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{i}: {Options[i].OptionName}");
            }
            Console.Write($"Enter a choice from 0-{Options.Count-1}: ");

            try
            {
                int choice = int.Parse(Console.ReadLine());
                return Options[choice];
            }
            catch (FormatException)
            {
                Console.WriteLine("That input was not recognized!");
                return ChooseOption();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("That was not one of the listed options!");
                return ChooseOption();
            }
        }

        public void Run(Actor self, Actor other=null)
        {
            Effect(self, other);
            if (Options.Count != 0)
                ChooseOption().Run(self, other);
            else
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}