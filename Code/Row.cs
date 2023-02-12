using System.Collections.Generic;

namespace DummyDb
{
    class Row
    {
        public List<Cell> CellsList { get; set; }

        public Row(string[] line, List<Column> columns, bool isFirst = false)
        {
            CellsList = new List<Cell>();

            for (int i = 0; i < line.Length; i++)
            {
                Cell cell = new Cell(columns[i], line[i], isFirst);
                CellsList.Add(cell);
            }
        }
    }
}
