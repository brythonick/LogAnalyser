using LogAnalyser;

namespace LogAnalyserTests
{
    public class DateRecorderTests
	{
		private readonly DateRecorder recorder = new();
        private readonly LogDate date = new(1, 1, 2024);

        [Fact]
        public void TakesDateObject()
		{
            Exception exception = Record.Exception(() => { recorder.Add(date); });
            Assert.Null(exception);
        }

        [Fact]
        public void MonthBinReturnsDateObject()
        {
            MonthBin bin = new();
            bin.Add(date);
            LogDate returnedDate = bin.Dates()[0];
            Assert.Equal((uint)2024, returnedDate.Year);
        }

        [Fact]
        public void MonthBinReturnsMultiplesInOrder()
        {
            MonthBin bin = new();
            LogDate d1 = new(1, 1, 2024);
            LogDate d2 = new(2, 1, 2024);
            LogDate d3 = new(3, 1, 2024);
            bin.Add(d2); bin.Add(d3); bin.Add(d1);
            LogDate[] returnedDates = bin.Dates();
            LogDate d1Returned = returnedDates[0];
            LogDate d2Returned = returnedDates[1];
            LogDate d3Returned = returnedDates[2];
            Assert.Equal((uint)1, d1Returned.Day);
            Assert.Equal((uint)2, d2Returned.Day);
            Assert.Equal((uint)3, d3Returned.Day);
        }

        [Fact]
        public void YearBinReturnsDateObject()
        {
            YearBin bin = new(null);
            bin.Add(date);
            LogDate returnedDate = bin.Dates()[0];
            Assert.Equal((uint)2024, returnedDate.Year);
        }

        [Fact]
        public void YearBinReturnsMultiplesInOrder()
        {
            YearBin bin = new(null);
            LogDate d1 = new(1, 1, 2024);
            LogDate d2 = new(10, 2, 2024);
            LogDate d3 = new(3, 3, 2024);
            bin.Add(d2); bin.Add(d3); bin.Add(d1);
            LogDate[] returnedDates = bin.Dates();
            LogDate d1Returned = returnedDates[0];
            LogDate d2Returned = returnedDates[1];
            LogDate d3Returned = returnedDates[2];
            Assert.Equal((uint)1, d1Returned.Day);
            Assert.Equal((uint)10, d2Returned.Day);
            Assert.Equal((uint)3, d3Returned.Day);
        }

        [Fact]
        public void RecorderReturnsDateObjects()
        {
            recorder.Add(date);
            LogDate returnedDate = recorder.Dates()[0];
            Assert.Equal((uint)2024, returnedDate.Year);
        }

        [Fact]
        public void RecorderReturnsMultiplesInOrder()
        {
            LogDate d1 = new(23, 9, 1970);
            LogDate d2 = new(2, 4, 1995);
            LogDate d3 = new(15, 12, 2023);
            recorder.Add(d2); recorder.Add(d3); recorder.Add(d1);
            LogDate[] returnedDates = recorder.Dates();
            LogDate d1Returned = returnedDates[0];
            LogDate d2Returned = returnedDates[1];
            LogDate d3Returned = returnedDates[2];
            Assert.Equal((uint)23, d1Returned.Day);
            Assert.Equal((uint)2, d2Returned.Day);
            Assert.Equal((uint)15, d3Returned.Day);
        }
    }
}

