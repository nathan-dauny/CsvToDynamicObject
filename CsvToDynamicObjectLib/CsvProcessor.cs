using System.Globalization;
using CsvHelper;
using CSVtoObject;

namespace CSVtoDynamicObjectLib
{
    public class CsvProcessor
    {
        public List<FinalObject> Rows { get; private set; }
        public Dictionary<string, Type> ColumnTypes { get; private set; }

        public CsvProcessor()
        {
            Rows = new List<FinalObject>();
            ColumnTypes = new Dictionary<string, Type>();
        }

        public void LoadCsv(Stream csvStream)
        {
            List<Dictionary<string, string>> rawRows = new List<Dictionary<string, string>>();

            using (var reader = new StreamReader(csvStream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();
                foreach (var record in records)
                {
                    var dict = new Dictionary<string, string>();
                    var d = (IDictionary<string, object>)record;
                    foreach (var kvp in d)
                        dict[kvp.Key] = kvp.Value?.ToString();
                    rawRows.Add(dict);
                }
            }

            if (!rawRows.Any())
                return;

            // Détecter les types par colonne
            var columns = rawRows.First().Keys;
            foreach (var col in columns)
            {
                var colValues = rawRows.Select(r => r[col]);
                var detectedType = CsvTypeDetector.DetectColumnType(colValues);
                ColumnTypes[col] = detectedType;
            }

            // Convertir les valeurs dans le type détecté
            foreach (var rawRow in rawRows)
            {
                var typedFields = new Dictionary<string, object>();
                foreach (var col in columns)
                {
                    var val = rawRow[col];
                    var type = ColumnTypes[col];
                    typedFields[col] = CsvTypeDetector.ConvertToType(val, type);
                }
                Rows.Add(new FinalObject(typedFields));
            }
        }
    }
}
