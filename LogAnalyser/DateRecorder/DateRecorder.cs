namespace LogAnalyser
{
    public class DateRecorder
	{
        private YearBin[] _bins = new YearBin[55];

        public DateRecorder()
        {
            for (int i = 0; i < _bins.Length; i++)
            {
                _bins[i] = new YearBin();
            }
        }

        public void Add(LogDate date)
        {
            int index = (int)date.Year % 1970;
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

