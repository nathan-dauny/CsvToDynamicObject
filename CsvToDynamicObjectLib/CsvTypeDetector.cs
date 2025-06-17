using System.Globalization;

namespace CSVtoObject
{
    public static class CsvTypeDetector
    {
        private static readonly Type[] CandidateTypes = new Type[]
        {
            typeof(int),
            typeof(double),
            typeof(bool),
            typeof(DateTime),
            typeof(string)
        };

        public static Type DetectColumnType(IEnumerable<string> values)
        {
            foreach (var type in CandidateTypes)
            {
                if (type == typeof(string))
                    continue;

                if (values.All(val => string.IsNullOrWhiteSpace(val) || TryConvert(val, type)))
                    return type;
            }
            return typeof(string);
        }

        private static bool TryConvert(string val, Type type)
        {
            try
            {
                if (type == typeof(int))
                    int.Parse(val, NumberStyles.Integer, CultureInfo.InvariantCulture);
                else if (type == typeof(double))
                    double.Parse(val, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
                else if (type == typeof(bool))
                    bool.Parse(val);
                else if (type == typeof(DateTime))
                    DateTime.Parse(val, CultureInfo.InvariantCulture);
                else
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static object ConvertToType(string val, Type type)
        {
            if (string.IsNullOrWhiteSpace(val))
                return null;

            try
            {
                if (type == typeof(int))
                    return int.Parse(val, NumberStyles.Integer, CultureInfo.InvariantCulture);
                else if (type == typeof(double))
                    return double.Parse(val, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
                else if (type == typeof(bool))
                    return bool.Parse(val);
                else if (type == typeof(DateTime))
                    return DateTime.Parse(val, CultureInfo.InvariantCulture);
                else
                    return val;
            }
            catch
            {
                return val;
            }
        }
    }
}
