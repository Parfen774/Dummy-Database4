namespace DummyDb
{
    class Program
    {
        public static void Main(string[] args)
        {
            Table Teams = new Table(@"JSON/Team.json", @"CSV/Teams.csv");
            Table Cities = new Table(@"JSON/City.json", @"CSV/Cities.csv");
            Table Matches = new Table(@"JSON/Match.json", @"CSV/Matches.csv");
        }
    }
}