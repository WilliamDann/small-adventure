using System;
using System.IO;

namespace Adventure
{
    public class Event
    {
        public Action runnable { get; set; }
        public string message  { get; set; }

        public Event() { }
        public Event(string message)
        {
            this.message = message;
            this.runnable = delegate() {  };
        }
        public Event(string message, Action runnable)
        {
            this.message = message;
            this.runnable = runnable;
        }

        public virtual void Run()
        {
            Console.Clear();
            Console.WriteLine(message);

            if (runnable != null)
                runnable();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    public class TextFileEvent : Event
    {
        public TextFileEvent(string path, Action runnable=null)
        {
            try
            {
                this.message = File.ReadAllText(path);
            } catch (IOException)
            {
                Console.WriteLine($"Menu '{path}' could not be loaded!");
                this.message = "Menu file failed to load!";
            }

            this.runnable = runnable;
        }
    }
}