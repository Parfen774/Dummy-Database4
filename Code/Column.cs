namespace DummyDb
{
    class Column
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string IsPrimary { get; set; }
        public string IsForeign { get; set; }
        public string ReferencedTable { get; set; }

        public Column(string name, string type, string isPrimary, string isForeign, string referencedTable)
        {
            Name = name;
            Type = type;
            IsPrimary = isPrimary;
            IsForeign = isForeign;
            ReferencedTable = referencedTable;
        }
    }
}