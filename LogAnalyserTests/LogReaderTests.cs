using LogAnalyser;

namespace LogAnalyserTests
{
    public class LogReaderTests
	{
        [Fact]
        public void ReadsSpecificationFromConfig()
        {
            LogReader reader = new("ceredigion");
            Assert.Equal("ceredigion", reader.SpecName);
            LogSpec spec = reader.Spec;
            Assert.Equal(2, spec.indicies.yearStart);
            Assert.Equal(5, spec.indicies.yearEnd);
        }
    }
}

