using CsvToDynamicObjectLib;
using FluentAssertions;

namespace CsvToDynamicObjectTests
{
    public class CsvTypeDetectorTests
    {
        [Fact]
        public void DetectColumnType_ShouldDetectInt()
        {
            var values = new List<string> { "1", "2", "3", "42" };
            var type = CsvTypeDetector.DetectColumnType(values);
            type.Should().Be(typeof(int));
        }

        [Fact]
        public void DetectColumnType_ShouldDetectDouble()
        {
            var values = new List<string> { "1.5", "2.0", "3.14" };
            var type = CsvTypeDetector.DetectColumnType(values);
            type.Should().Be(typeof(double));
        }

        [Fact]
        public void DetectColumnType_ShouldDetectBool()
        {
            var values = new List<string> { "true", "false", "True" };
            var type = CsvTypeDetector.DetectColumnType(values);
            type.Should().Be(typeof(bool));
        }

        [Fact]
        public void DetectColumnType_ShouldDetectDateTime()
        {
            var values = new List<string> { "2023-01-01", "2024-12-31" };
            var type = CsvTypeDetector.DetectColumnType(values);
            type.Should().Be(typeof(DateTime));
        }

        [Fact]
        public void DetectColumnType_ShouldFallbackToString()
        {
            var values = new List<string> { "hello", "world", "123abc" };
            var type = CsvTypeDetector.DetectColumnType(values);
            type.Should().Be(typeof(string));
        }

        [Theory]
        [InlineData("123", typeof(int))]
        [InlineData("123.45", typeof(double))]
        [InlineData("true", typeof(bool))]
        [InlineData("2020-06-01", typeof(DateTime))]
        [InlineData("hello", typeof(string))]
        public void ConvertToType_ShouldConvertProperly(string input, Type expectedType)
        {
            var result = CsvTypeDetector.ConvertToType(input, expectedType);
            result.Should().NotBeNull();

            switch (expectedType.Name)
            {
                case "Int32":
                    result.Should().BeOfType<int>();
                    result.Should().Be(int.Parse(input));
                    break;
                case "Double":
                    result.Should().BeOfType<double>();
                    result.Should().Be(double.Parse(input));
                    break;
                case "Boolean":
                    result.Should().BeOfType<bool>();
                    result.Should().Be(bool.Parse(input));
                    break;
                case "DateTime":
                    result.Should().BeOfType<DateTime>();
                    result.Should().Be(DateTime.Parse(input));
                    break;
                case "String":
                    result.Should().Be(input);
                    break;
            }
        }

        [Fact]
        public void ConvertToType_ShouldReturnNullForEmptyOrWhitespace()
        {
            CsvTypeDetector.ConvertToType("", typeof(int)).Should().BeNull();
            CsvTypeDetector.ConvertToType(" ", typeof(double)).Should().BeNull();
            CsvTypeDetector.ConvertToType(null, typeof(DateTime)).Should().BeNull();
        }

        [Fact]
        public void ConvertToType_ShouldReturnStringIfConversionFails()
        {
            var invalidInt = "abc";
            var result = CsvTypeDetector.ConvertToType(invalidInt, typeof(int));
            result.Should().Be(invalidInt);
        }
    }
}