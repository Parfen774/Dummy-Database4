using System;
using System.Collections.Generic;
using System.Text;

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

            string[][] dataCSV = ReaderCSV.ReadCSV(csvPath);

            for (int i = 0; i < dataCSV.Length; i++)
            {
                string[] line = dataCSV[i];
                if (i == 0) Rows.Add(new Row(line, Scheme.Columns, true));
                else Rows.Add(new Row(line, Scheme.Columns));
            }
                
        }

        public void Print()
        {
            foreach (Row row in Rows)
            {
                StringBuilder result = new StringBuilder();
                int[] maxColumns = MaxLengthColumns();

                for (int i = 0; i < row.CellsList.Count; i++)
                {
                    string data = row.CellsList[i].Data.ToString();
                    result.Append($"|{data.PadRight(maxColumns[i])}|");
                }

                Console.WriteLine(result.Replace("||", "|"));

                if (Rows.IndexOf(row) == 0)
                    PrintSeparator(maxColumns);
            }
        }

        private int[] MaxLengthColumns()
        {
            int[] result = new int[Scheme.Columns.Count];

            for (int i = 0; i < Rows.Count; i++)
                for (int j = 0; j < result.Length; j++)
                {
                    int len = Rows[i].CellsList[j].Data.ToString().Length;
                    result[j] = result[j] < len ? len : result[j]; 
                }

            return result;
        }

        private void PrintSeparator(int[] maxColumns)
        {
            StringBuilder separator = new StringBuilder();

            foreach (int len in maxColumns)
                separator.Append($"|{new string('-', len)}|");

            Console.WriteLine(separator.Replace("||", "|"));
        }
    }
}