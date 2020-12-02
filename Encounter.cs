using System;
using System.Collections.Generic;

namespace Adventure
{
    public class TextMenu
    {
        public string               Message { get; private set; }
        public Action<Actor, Actor> Effect  { get; private set; }
        public List<TextMenu>       Options { get; private set; }

        public TextMenu(string Message) 
            : this(Message, (a, b) => {}, null) { }
        public TextMenu(string Message, Action<Actor, Actor> Effect)
            : this(Message, Effect, null) { }

        public TextMenu(string Message, Action<Actor, Actor> Effect, List<TextMenu> Options)
        {
            this.Message = Message;
            this.Effect  = Effect;
            this.Options = Options;
        }

        public TextMenu ChooseOption()
        {
            
        }

        public void Run(Actor self, Actor other=null)
        {
            Console.WriteLine(this.Message);
            Effect(self, other);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            if (Options.Count != 0)
                ChooseOption().Run(self, other);
        }
    }
}