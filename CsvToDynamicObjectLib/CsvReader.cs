using System.Globalization;
using CsvHelper;

namespace CSVtoDynamicObjectLib
{
    /// <summary>
    /// Reads CSV data from a stream and returns raw rows as dictionaries.
    /// </summary>
    public class CsvReader
    {
        /// <summary>
        /// Reads CSV data from the given stream.
        /// </summary>
        /// <param name="csvStream">Stream containing CSV data.</param>
        /// <returns>List of rows represented as dictionaries of column-name to string value.</returns>
        public List<Dictionary<string, string>> ReadCsv(Stream csvStream)
        {
            var rows = new List<Dictionary<string, string>>();

            using (var reader = new StreamReader(csvStream))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();

                foreach (var record in records)
                {
                    var dict = new Dictionary<string, string>();
                    var d = (IDictionary<string, object>)record;

                    foreach (var kvp in d)
                    {
                        dict[kvp.Key] = kvp.Value?.ToString();
                    }

                    rows.Add(dict);
                }
            }
            return rows;
        }
    }
}