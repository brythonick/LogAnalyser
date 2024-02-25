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
            Assert.Equal(0, spec.indicies.yearStart);
            Assert.Equal(3, spec.indicies.yearEnd);
        }

        [Theory]
        [InlineData("ceredigion", "1,2024-01-01T08:15:30,Info,ApplicationA", 2024, 1, 1)]
        [InlineData("ceredigion", "26,2024-01-24T13:10:42,Debug,ComponentZ", 2024, 1, 24)]
        [InlineData("ceredigion", "59,2024-02-26T11:45:30,Info,ApplicationC", 2024, 2, 26)]
        [InlineData("ceredigion", "118,2024-04-25T09:53:42,Debug,ComponentZ", 2024, 4, 25)]
        [InlineData("ceredigion", "150,2024-05-27T09:53:42,Debug,ComponentZ", 2024, 5, 27)]
        [InlineData("spark", "17/02/09 20:10:40 INFO executor.CoarseGrainedExecutorBackend: Registered signal handlers for [TERM, HUP, INT]", 2009, 2, 17)]
        [InlineData("spark", "27/03/09 20:10:48 INFO storage.MemoryStore: Block broadcast_3_piece0 stored as bytes in memory (estimated size 152.0 B, free 318.1 KB)", 2009, 3, 27)]
        [InlineData("spark", "02/06/09 20:10:48 INFO executor.Executor: Finished task 0.0 in stage 0.0 (TID 0). 2703 bytes result sent to driver", 2009, 6, 2)]
        [InlineData("spark", "01/10/11 20:10:57 INFO storage.BlockManager: Found block rdd_11_4 locally", 2011, 10, 1)]
        [InlineData("spark", "30/01/16 20:11:08 INFO rdd.HadoopRDD: Input split: hdfs://10.10.34.11:9000/pjhe/logs/2kSOSP.log:153132+7292", 2016, 1, 30)]
        public void ParsesDatesCorrectly(string specName, string logLine, uint year, uint month, uint day)
        {
            LogReader reader = new(specName);
            LogDate date = reader.ParseDate(logLine);
            Assert.Equal(year, date.Year);
            Assert.Equal(month, date.Month);
            Assert.Equal(day, date.Day);
        }
    }
}

