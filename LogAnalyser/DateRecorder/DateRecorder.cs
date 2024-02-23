namespace LogAnalyser
{
    public class DateRecorder
	{
        private YearBin[] _bins = new YearBin[1];
        private int _smallestYear = 0;

        public void Add(LogDate date)
        {
            if (_smallestYear == 0)
            {
                _smallestYear = (int)date.Year;
                _bins[0] = new YearBin();
            }
            int index = (int)date.Year % _smallestYear;
            if (index == (int)date.Year)
            {
                _bins = Merge.YearBinArrays(new YearBin[1] { new YearBin() }, _bins);
                _bins[0].Add(date);
                _smallestYear = (int)date.Year;
            }
            else if (index > _bins.Length)
            {
                _bins = Merge.YearBinArrays(_bins, new YearBin[1] { new YearBin() });
                _bins[_bins.Length - 1].Add(date);
            }
            else
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

