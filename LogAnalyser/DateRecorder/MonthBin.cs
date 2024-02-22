namespace LogAnalyser
{
    public class MonthBin
	{
        private LogDate[] _bins = new LogDate[31];
        private int _totalDates = 0;

        public MonthBin()
        {
            for (int i = 0; i < _bins.Length; i++)
            {
                _bins[i] = new LogDate();
            }
        }

        public void Add(LogDate date)
        {
            int index = (int)date.Day - 1;
            _bins[index] = date;
            _totalDates++;
        }

        public LogDate[] Dates()
        {
            LogDate[] recordedDates = new LogDate[_totalDates];
            int recordedDateIndex = 0;
            for (int i = 0; i < _bins.Length; i++)
            {
                LogDate current = _bins[i];
                if (!current.Empty)
                {
                    recordedDates[recordedDateIndex] = current;
                    recordedDateIndex++;
                }
            }
            return recordedDates;
        }
    }
}

