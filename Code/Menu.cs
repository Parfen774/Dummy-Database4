using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DummyDb
{
    class Menu
    {
        public string[] Items { get; set; }
        public int Point { get; set; }

        public Menu(string pathJSON, string pathCSV)
        {
            Items = GetListScheme(pathJSON, pathCSV);
            Point = 0;
        }

        public void Print()
        {
            Console.Clear();
            Console.WriteLine("Доступные таблицы:");
            for (int i = 0; i < Items.Length; i++)
            {
                string result = Items[i];
                if (Point == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    result = "  " + result;
                }
                Console.WriteLine(result);
                Console.ResetColor();
            }
        }

        public void Update(string key)
        {
            switch (key)
            {
                case "UpArrow":
                    Point--;
                    break;
                case "DownArrow":
                    Point++;
                    break;
                case "Q":
                    System.Environment.Exit(0);
                    break;
                case "Enter":
                    break;
            }

            if (Point < 0)
                Point = Items.Length - 1;
            else if (Point > Items.Length - 1)
                Point = 0;
        }

        private string[] GetListScheme(string pathJSON, string pathCSV)
        {
            string[] schemeFiles = Directory.GetFiles($@"{pathJSON}");
            string[] dataFiles = Directory.GetFiles(@$"{pathCSV}");

            List<string> result = new List<string>();

            for (int i = 0; i < schemeFiles.Length; i++)
                schemeFiles[i] = Path.GetFileNameWithoutExtension(schemeFiles[i]);

            for (int i = 0; i < dataFiles.Length; i++)
                dataFiles[i] = Path.GetFileNameWithoutExtension(dataFiles[i]);

            foreach (string nameScheme in schemeFiles)
                if (dataFiles.Contains(nameScheme))
                    result.Add(nameScheme);

            return result.ToArray();
        }
    }
}
