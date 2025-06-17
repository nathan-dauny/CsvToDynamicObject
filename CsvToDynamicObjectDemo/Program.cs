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

            var processor = new CsvProcessor();
            processor.LoadCsv(fileStream);

            Console.WriteLine("Colonnes et types détectés :");
            foreach (var colType in processor.ColumnTypes)
                Console.WriteLine($"{colType.Key} : {colType.Value.Name}");

            Console.WriteLine("\nLignes typées :");
            foreach (var row in processor.Rows)
                Console.WriteLine(row);
        }
    }
}
