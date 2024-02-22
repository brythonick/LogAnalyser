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
            return recordedDates;
        }
    }
}

