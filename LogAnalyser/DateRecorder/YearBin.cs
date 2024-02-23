namespace LogAnalyser
{
    public class YearBin
	{
        private MonthBin[] _bins = new MonthBin[12];
        private int _year;

        public YearBin(int? year)
        {
            if (year != null)
                _year = year.Value;
            for (int i = 0; i < _bins.Length; i++)
            {
                _bins[i] = new MonthBin();
            }
        }

        public override string ToString()
        {
            if (_year != 0)
                return string.Format("YearBin {0:0000}", _year);
            else
                return "YearBin Empty";
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

