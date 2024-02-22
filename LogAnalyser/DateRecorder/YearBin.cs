namespace LogAnalyser
{
    public class YearBin
	{
        private MonthBin[] _bins = new MonthBin[12];
        private bool[] _populatedBins = new bool[12];

        public YearBin()
        {
            for (int i = 0; i < _bins.Length; i++)
            {
                _bins[i] = new MonthBin();
                _populatedBins[i] = false;
            }
        }

        public void Add(LogDate date)
        {
            int index = (int)date.Month - 1;
            _bins[index].Add(date);
            _populatedBins[index] = true;
        }

        public LogDate[] Dates()
        {
            LogDate[] recordedDates = new LogDate[0];
            for (int i = 0; i < _bins.Length; i++)
            {
                if (_populatedBins[i])
                {
                    LogDate[] currentRecordedDates = _bins[i].Dates();
                    LogDate[] newRecordedDates = new LogDate[recordedDates.Length + currentRecordedDates.Length];
                    for (int j = 0; j < recordedDates.Length; j++)
                    {
                        newRecordedDates[j] = recordedDates[j];
                    }
                    for (int j = 0; j < currentRecordedDates.Length; j++)
                    {
                        newRecordedDates[j + recordedDates.Length] = currentRecordedDates[j];
                    }
                    recordedDates = newRecordedDates;
                }
            }
            return recordedDates;
        }
    }
}

