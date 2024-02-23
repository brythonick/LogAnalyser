namespace LogAnalyser
{
    public class YearBin
	{
        private MonthBin[] _bins = new MonthBin[12];

        public YearBin()
        {
            for (int i = 0; i < _bins.Length; i++)
            {
                _bins[i] = new MonthBin();
            }
        }

        public void Add(LogDate date)
        {
            int index = (int)date.Month - 1;
            _bins[index].Add(date);
        }

        public LogDate[] Dates()
        {
            LogDate[] recordedDates = new LogDate[0];
            for (int i = 0; i < _bins.Length; i++)
            {
                recordedDates = Merge.LogDateArrays(recordedDates, _bins[i].Dates());
            }
            return recordedDates;
        }
    }
}

