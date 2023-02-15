using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DummyDb
{
    class Scheme
    {
        public string Name { get; set; }
        public List<Column> Columns { get; set; }

        public Scheme(string path)
        {
            JObject jsonScheme = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(path));
            JArray jColumns = (JArray)jsonScheme["columns"];

            Name = jsonScheme["name"].ToString();
            Columns = new List<Column>();

            foreach (var item in jColumns)
            {
                Columns.Add(new Column
                    (
                    item["name"].ToString(),
                    item["type"].ToString(),
                    item["isPrimary"].ToString(),
                    item["isForeign"].ToString(),
                    (string)item["referencedTable"]
                    ));
            }
        }
    }
}