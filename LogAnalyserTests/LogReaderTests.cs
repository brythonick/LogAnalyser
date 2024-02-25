using LogAnalyser;

namespace LogAnalyserTests
{
    public class LogReaderTests
	{
        private LogReader reader = new("ceredigion");

        [Fact]
        public void ReadsSpecificationFromConfig()
        {
            Assert.Equal("ceredigion", reader.SpecName);
            LogSpec spec = reader.Spec;
            Assert.Equal(0, spec.indicies.yearStart);
            Assert.Equal(3, spec.indicies.yearEnd);
        }

        [Theory]
        [InlineData("1,2024-01-01T08:15:30,Info,ApplicationA", 2024, 1, 1)]
        [InlineData("26,2024-01-24T13:10:42,Debug,ComponentZ", 2024, 1, 24)]
        [InlineData("59,2024-02-26T11:45:30,Info,ApplicationC", 2024, 2, 26)]
        [InlineData("118,2024-04-25T09:53:42,Debug,ComponentZ", 2024, 4, 25)]
        [InlineData("150,2024-05-27T09:53:42,Debug,ComponentZ", 2024, 5, 27)]
        public void ParsesDatesCorrectly(string logLine, uint year, uint month, uint day)
        {
            LogDate date = reader.ParseDate(logLine);
            Assert.Equal(year, date.Year);
            Assert.Equal(month, date.Month);
            Assert.Equal(day, date.Day);
        }
    }
}

