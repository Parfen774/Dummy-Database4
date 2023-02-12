using System.Collections.Generic;

namespace DummyDb
{
    class Table
    {
        public List<Row> Rows { get; set; }
        public Scheme Scheme { get; set; }

        public Table(string jsonPath, string csvPath)
        {
            Scheme = new Scheme(jsonPath);
            Rows = new List<Row>();

            string[][] dataCSV = ReaderCSV.ReadCSV(csvPath)[1..];

            foreach (string[] line in dataCSV)
                Rows.Add(new Row(line, Scheme.Columns));
        }
    }
}