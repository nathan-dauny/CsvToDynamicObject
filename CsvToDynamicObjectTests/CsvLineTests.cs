using CsvToDynamicObjectLib;
using FluentAssertions;

namespace CsvToDynamicObjectTests
{
    public class CsvLineTests
    {
        [Fact]
        public void GetField_ShouldReturnValueIfExists()
        {
            var dict = new Dictionary<string, object>
        {
            { "Name", "Alice" },
            { "Age", 30 }
        };

            var line = new CsvLine(dict);
            line.GetField("Name").Should().Be("Alice");
            line.GetField("Age").Should().Be(30);
        }

        [Fact]
        public void GetField_ShouldReturnNullIfNotExists()
        {
            var line = new CsvLine(new Dictionary<string, object>());
            line.GetField("Missing").Should().BeNull();
        }

        [Fact]
        public void ToString_ShouldReturnKeyValuePairs()
        {
            var dict = new Dictionary<string, object>
        {
            { "Name", "Alice" },
            { "Age", 30 }
        };
            var line = new CsvLine(dict);
            var str = line.ToString();
            str.Should().Contain("Name=Alice");
            str.Should().Contain("Age=30");
        }
    }
}
