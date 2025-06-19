using System.Text;
using CsvToDynamicObjectLib;
using FluentAssertions;

namespace CsvToDynamicObjectTests
{
    public class CsvReaderConverterTests
    {
        [Fact]
        public void ReadCsv_ShouldParseCsvCorrectly()
        {
            var csvContent = "Name,Age,IsActive\nAlice,30,true\nBob,25,false\n";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));

            var reader = new CsvReaderConverter();
            var result = reader.ReadCsv(stream);

            result.Should().HaveCount(2);
            result[0]["Name"].Should().Be("Alice");
            result[0]["Age"].Should().Be("30");
            result[0]["IsActive"].Should().Be("true");

            result[1]["Name"].Should().Be("Bob");
            result[1]["Age"].Should().Be("25");
            result[1]["IsActive"].Should().Be("false");
        }
    }
}
