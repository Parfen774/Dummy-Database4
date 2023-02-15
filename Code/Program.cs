using System;

namespace DummyDb
{
    class Program
    {
        public static void Main(string[] args)
        {
            Menu Menu = new Menu(@"JSON", @"CSV");
            
            while (true)
            {
                Menu.Print();
                var pressButton = Console.ReadKey();
                Menu.Update(pressButton.Key.ToString());
            }
        }
    }
}