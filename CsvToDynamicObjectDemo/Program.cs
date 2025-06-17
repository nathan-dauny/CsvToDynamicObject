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
                Console.WriteLine("Fichier CSV non trouvé !");
                return;
            }
            using var fileStream = File.OpenRead(filePath);

            //var processor = new CsvFinalObject();
            //processor.LoadCsv(fileStream);

            var reader = new CsvReader();
            var csvRead = reader.ReadCsv(fileStream);
            var columnstype = new ColumnsType();
            var columnstypeDico = columnstype.GetAllColumnsTypes(csvRead);
            var csvTyped = new CsvTyped();
            var csvTypedFields = csvTyped.GetFieldsTyped(columnstypeDico,csvRead);
            var csvFinalObject2 = new CsvFinalObject2(csvTypedFields);

            Console.WriteLine("Columns Type detected :");
            foreach (var colType in columnstypeDico)
                Console.WriteLine($"{colType.Key} : {colType.Value.Name}");

            Console.WriteLine("\nTyped Lines :");
            foreach (var row in csvFinalObject2)
                Console.WriteLine(row);
        }
    }
}
