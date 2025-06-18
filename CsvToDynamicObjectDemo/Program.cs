using System.Collections.Generic;
using System.Text;
using CsvToDynamicObjectLib;
using CSVtoDynamicObjectLib;

namespace CSVtoObject
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
            var csvRead = reader.ReadCsv(fileStream);
            var columnstype = new ColumnsType();
            var columnstypeDico = columnstype.GetAllColumnsTypes(csvRead);
            var csvTyped = new CsvTyped();
            var csvTypedFields = csvTyped.GetFieldsTyped(columnstypeDico,csvRead);
            var csvFinalObject = new CsvFinalObject(csvTypedFields);

            List<string> insertQueries = new List<string>();

            var firstPartInsertQuery = new StringBuilder();
            firstPartInsertQuery.Append("INSERT INTO table (");
            foreach (var kvp in columnstypeDico)
            {
                firstPartInsertQuery.Append(kvp.Key)
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
