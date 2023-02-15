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
                bool flag = false;

                if (i == 0) flag = true;

                Rows.Add(new Row(line, Scheme.Columns, flag));
            }
                
        }

        public void Print()
        {
            List<string[]> allLines = GetAllLines();
            int[] maxColumns = MaxLengthColumns(allLines);

            foreach (string[] line in allLines)
            {
                StringBuilder result = new StringBuilder();

                for (int i = 0; i < line.Length; i++)
                {
                    result.Append($"|{line[i].PadRight(maxColumns[i])}|");
                    result.Replace("||", "|");
                }

                Console.WriteLine(result.ToString());
                if (allLines.IndexOf(line) == 0)
                    PrintSeparator(maxColumns);
            }
        }

        private List<string[]> GetAllLines()
        {
            List<string[]> result = new List<string[]>();

            foreach (Row row in Rows)
            {
                string[] line = new string[row.CellsList.Count];

                for (int i = 0; i < row.CellsList.Count; i++)
                {
                    Cell cell = row.CellsList[i];

                    if (cell.Column.ReferencedTable is null)
                        line[i] = cell.Data.ToString();
                    else
                        line[i] = RequestAnotherTable(cell.Column.ReferencedTable, cell.Data.ToString());
                }

                result.Add(line);
            }

            return result;
        }

        private int[] MaxLengthColumns(List<string[]> allLines)
        {
            int[] result = new int[allLines[0].Length];

            foreach (string[] line in allLines)
                for (int i = 0; i < line.Length; i++)
                    if (result[i] < line[i].Length)
                        result[i] = line[i].Length;

            return result;
        }

        private void PrintSeparator(int[] maxColumns)
        {
            StringBuilder separator = new StringBuilder();

            foreach (int len in maxColumns)
                separator.Append($"|{new string('-', len)}|");

            Console.WriteLine(separator.Replace("||", "|"));
        }

        private string RequestAnotherTable(string nameTable, string primaryKey)
        {
            Table requestTable = new Table($@"JSON/{nameTable}.json", $@"CSV/{nameTable}.csv");
            List<Column> columnsScheme = requestTable.Scheme.Columns;
            int indexPK = 0;
            int indexFK = 0;

            foreach (Column column in columnsScheme)
            {
                if (bool.Parse(column.IsPrimary))
                    indexPK = columnsScheme.IndexOf(column);
                if (bool.Parse(column.IsForeign))
                    indexFK = columnsScheme.IndexOf(column);
            }

            foreach (Row row in requestTable.Rows)
            {
                Cell cell = row.CellsList[indexPK];
                if (cell.Data.ToString() == primaryKey)
                    return row.CellsList[indexFK].Data.ToString();
            }

            return columnsScheme[indexFK].Name.ToString();
        }
    }
}