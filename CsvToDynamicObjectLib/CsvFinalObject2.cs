using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVtoObject;

namespace CsvToDynamicObjectLib
{
    public class CsvFinalObject2 : IEnumerable<CsvLine>
    {
        public List<CsvLine> csvObject { get; private set; } = new List<CsvLine>();
        public CsvFinalObject2(List<Dictionary<string, object>> csvTyped)
        {
            csvObject=csvTyped.Select(kvp=> new CsvLine(kvp)).ToList();
        }
        public IEnumerator<CsvLine> GetEnumerator() => csvObject.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
