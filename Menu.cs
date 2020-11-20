using System.Collections.Generic;
using System.IO;
using System;

namespace Adventure
{
    public interface IMenu
    {
        void ShowMenu();
    }

    public class TextFileMenu : IMenu
    {
        string message;

        public TextFileMenu(string path)
        {
            try
            {
                this.message = File.ReadAllText(path);
            } catch (IOException)
            {
                Console.WriteLine($"Menu '{path}' could not be loaded!");
                this.message = "Menu file failed to load!";
            }
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}