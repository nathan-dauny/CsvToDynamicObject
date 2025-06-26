using System.Collections.Concurrent;
using System.Text;
using CsvToDynamicObjectLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CsvToDynamicObject
{
    class Program
    {
        static void Main()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inputs", "example1.csv");
            if (!File.Exists(filePath))
            {
                Console.WriteLine("CSV not found");
                return;
            }
            using var fileStream = File.OpenRead(filePath);
            var reader = new CsvReaderConverter();

            var streamReader = new StreamReader(fileStream);
            var (csvFinalObject, columnstypeDico) = ReturnObject.GetObjectsCSV(streamReader);

            List<string> insertQueries = new List<string>();
            var test = csvFinalObject.First().Select(kvp=>kvp.Key).ToList();
            var firstPartInsertQuery = new StringBuilder();
            firstPartInsertQuery.Append("INSERT INTO table (");

            foreach (var k in test)
            {
                firstPartInsertQuery.Append(k)
                    .Append(',');
            }
            firstPartInsertQuery.Length--;
            firstPartInsertQuery.Append(") VALUES (");

            foreach (var csvLine in csvFinalObject)
            {
                var insertLine = new StringBuilder(firstPartInsertQuery.ToString());
                foreach (var kvp in csvLine)
                {
                    insertLine.Append("'")
                        .Append(kvp.Value != null ? kvp.Value.ToString() : "")
                        .Append("', ");
                }
                insertLine.Remove(insertLine.Length - 2, 2)
                    .Append(')');
                insertQueries.Add(insertLine.ToString());
            }
            foreach(var insertQuery in insertQueries)
            {
                Console.WriteLine(insertQuery);
            }
            Console.WriteLine("Columns Type detected :");
            foreach (var colType in columnstypeDico)
                Console.WriteLine($"{colType.Key} : {colType.Value.Name}");

            Console.WriteLine("\nTyped Lines :");
            foreach (var row in csvFinalObject)
                Console.WriteLine(row);
        }
    }
}
