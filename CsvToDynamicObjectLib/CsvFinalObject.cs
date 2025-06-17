using System.Globalization;
using CsvHelper;
using CSVtoObject;

namespace CSVtoDynamicObjectLib
{
    /// <summary>
    /// Processes CSV data streams and converts them into typed dynamic objects.
    /// </summary>
    public class CsvFinalObject
    {
        /// <summary>
        /// Gets the list of parsed CSV rows represented as <see cref="CsvLine"/> instances.
        /// </summary>
        public List<CsvLine> Rows { get; private set; }

        /// <summary>
        /// Gets the detected data types for each CSV column.
        /// </summary>
        public Dictionary<string, Type> ColumnTypes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvFinalObject"/> class.
        /// </summary>
        public CsvFinalObject()
        {
            Rows = new List<CsvLine>();
            ColumnTypes = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Loads and parses CSV data from a stream, detecting column types and converting values accordingly.
        /// </summary>
        /// <param name="csvStream">The stream containing the CSV data.</param>
        public void LoadCsv(Stream csvStream)
        {
            List<Dictionary<string, string>> rawRows = new List<Dictionary<string, string>>();

            using (var reader = new StreamReader(csvStream))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
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

            // Detect column types
            var columns = rawRows.First().Keys;
            foreach (var col in columns)
            {
                var colValues = rawRows.Select(r => r[col]);
                var detectedType = CsvTypeDetector.DetectColumnType(colValues);
                ColumnTypes[col] = detectedType;
            }

            // Convert values to detected types and store as FinalObject instances
            foreach (var rawRow in rawRows)
            {
                var typedFields = new Dictionary<string, object>();
                foreach (var col in columns)
                {
                    var val = rawRow[col];
                    var type = ColumnTypes[col];
                    typedFields[col] = CsvTypeDetector.ConvertToType(val, type);
                }
                Rows.Add(new CsvLine(typedFields));
            }
        }
    }
}
