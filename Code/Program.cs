namespace DummyDb
{
    class Program
    {
        public static void Main(string[] args)
        {
            Table Teams = new Table(@"JSON/Team.json", @"CSV/Teams.csv");
            Teams.Print();
        }
    }
}