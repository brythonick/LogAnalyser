namespace LogAnalyser
{
    public class LogDate
	{
		private uint _day;
		public uint day
		{
			get { return _day; }
			set
			{
				if (value == 0)
					throw new DateDayZero();
				else if (value > 31)
					throw new DateDayTooLarge();
				else
					_day = value;
			}
		}

		private uint _month;
		public uint month
		{
			get { return _month; }
			set
			{
				if (value == 0)
					throw new DateMonthZero();
				else if (value > 12)
					throw new DateMonthTooLarge();
				else
					_month = value;
			}
		}

		private uint _year;
        public uint year
		{
			get { return _year; }
			set
			{
				if (value < 1970)
					throw new DateYearTooSmall();
				else
					_year = value;
			}
		}

		public LogDate() { }
        public LogDate(uint argDay, uint argMonth, uint argYear)
		{
			day = argDay; month = argMonth; year = argYear;
		}

		public static bool operator <(LogDate s, LogDate l)
		{
			return (s.year < l.year)
				|| ((s.year == l.year) && (s.month < l.month))
				|| ((s.year == l.year) && (s.month == l.month) && (s.day < l.day));
        }
        public static bool operator >(LogDate l, LogDate s)
        {
            return (s.year < l.year)
                || ((s.year == l.year) && (s.month < l.month))
                || ((s.year == l.year) && (s.month == l.month) && (s.day < l.day));
        }
    }

	public class DateDayZero : Exception { }
    public class DateDayTooLarge : Exception { }
	public class DateMonthZero : Exception { }
    public class DateMonthTooLarge : Exception { }
    public class DateYearTooSmall : Exception { }
}

