namespace DummyDb
{
    class Program
    {
        public static void Main(string[] args)
        {
            Table Match = new Table(@"JSON/Match.json", @"CSV/Match.csv");
            Match.Print();
        }
    }
}