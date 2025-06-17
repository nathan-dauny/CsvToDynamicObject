namespace CSVtoObject
{
    // Classe représentant une ligne CSV avec valeurs typées
    public class FinalObject
    {
        public Dictionary<string, object> Fields { get; }

        public FinalObject(Dictionary<string, object> fields)
        {
            Fields = fields;
        }

        public object GetField(string columnName)
        {
            if (Fields.ContainsKey(columnName))
            {
                return Fields[columnName];
            }
            else
            {
                return null;
            }
        }

        //Especiallu to display the dictionnary object
        public override string ToString()
        {
            return string.Join(", ", Fields.Select(kvp => $"{kvp.Key}={kvp.Value}"));

        }
    }
}
