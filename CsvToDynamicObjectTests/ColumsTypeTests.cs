using CsvToDynamicObjectLib;
using FluentAssertions;

namespace CsvToDynamicObjectTests
{
    public class ColumsTypeTests
    {
        [Fact]
        public void GetAllColumnsTypes_ShouldDetectTypesCorrectly()
        {
            var csvParsed = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { { "Id", "1" }, { "Price", "12.5" }, { "IsActive", "true" }, { "Date", "2023-06-01" }, { "Name", "Alice" } },
            new Dictionary<string, string> { { "Id", "2" }, { "Price", "8.3" }, { "IsActive", "false" }, { "Date", "2023-06-02" }, { "Name", "Bob" } },
            new Dictionary<string, string> { { "Id", "3" }, { "Price", "15.0" }, { "IsActive", "true" }, { "Date", "2023-06-03" }, { "Name", "Carol" } }
        };

            var columnsType = new ColumnsType();
            var types = columnsType.GetAllColumnsTypes(csvParsed);

            types.Should().Contain(new KeyValuePair<string, Type>("Id", typeof(int)));
            types.Should().Contain(new KeyValuePair<string, Type>("Price", typeof(double)));
            types.Should().Contain(new KeyValuePair<string, Type>("IsActive", typeof(bool)));
            types.Should().Contain(new KeyValuePair<string, Type>("Date", typeof(DateTime)));
            types.Should().Contain(new KeyValuePair<string, Type>("Name", typeof(string)));
        }
    }
}
