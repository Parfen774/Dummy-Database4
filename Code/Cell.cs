using System;

namespace DummyDb
{
    class Cell
    {
        public Column Column { get; set; }
        public object Data { get; set; }

        public Cell(Column column, string data, bool isFirst)
        {
            Column = column;
            Data = TryConvertType(Column.Type, data, isFirst);
        }

        private object TryConvertType(string type, string data, bool isFirst)
        {
            if (isFirst)
                return data;

            switch (type)
            {
                case "uint":
                    if (uint.TryParse(data, out uint id))
                        return id;
                    throw new Exception("Incorrect type in file");
                case "int":
                    if (int.TryParse(data, out int num))
                        return num;
                    throw new Exception("Incorrect type in file");
                case "DateTime":
                    if (DateTime.TryParse(data, out DateTime date))
                        return date.ToShortDateString();
                    throw new Exception("Incorrect type in file");
                default:
                    return data;
            }
        }
    }
}
