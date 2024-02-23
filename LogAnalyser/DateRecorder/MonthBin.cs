namespace LogAnalyser
{
    public class MonthBin
	{
        private LogDate[] _bins = new LogDate[31];
        private int _totalDates = 0;
        private uint _month;

        public MonthBin()
        {
            for (int i = 0; i < _bins.Length; i++)
            {
                _bins[i] = new LogDate();
            }
        }

        public override string ToString()
        {
            if (_totalDates != 0)
                return string.Format("MonthBin {0:00}", _month);
            else
                return "MonthBin Empty";
        }

        public void Add(LogDate date)
        {
            _month = date.Month;
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

