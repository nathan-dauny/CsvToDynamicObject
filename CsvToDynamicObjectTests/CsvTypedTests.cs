using CsvToDynamicObjectLib;
using FluentAssertions;

namespace CsvToDynamicObjectTests
{
    public class CsvTypedTests
    {
        [Fact]
        public void GetFieldsTyped_ShouldConvertAllFieldsToCorrectType()
        {
            var csvParsed = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { { "Id", "1" }, { "Price", "12.5" }, { "Name", "Alice" } },
            new Dictionary<string, string> { { "Id", "2" }, { "Price", "8.3" }, { "Name", "Bob" } },
        };

            var columnsType = new Dictionary<string, Type>
        {
            { "Id", typeof(int) },
            { "Price", typeof(double) },
            { "Name", typeof(string) }
        };

            var csvTyped = new CsvTyped();
            var result = csvTyped.GetFieldsTyped(columnsType, csvParsed);

            result.Should().HaveCount(2);
            result[0]["Id"].Should().BeOfType<int>().And.Be(1);
            result[0]["Price"].Should().BeOfType<double>().And.Be(12.5);
            result[0]["Name"].Should().Be("Alice");

            result[1]["Id"].Should().BeOfType<int>().And.Be(2);
            result[1]["Price"].Should().BeOfType<double>().And.Be(8.3);
            result[1]["Name"].Should().Be("Bob");
        }
    }
}
