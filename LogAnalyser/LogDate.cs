namespace LogAnalyser
{
    public class LogDate
	{
		private uint _day;
		public uint Day
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
		public uint Month
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
        public uint Year
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
			Day = argDay; Month = argMonth; Year = argYear;
		}

		public bool Empty { get { return _day == 0 && _month == 0 && _year == 0; } }

		public override string ToString()
		{
			if (_day != 0 && _month != 0 && _year != 0)
				return string.Format("{0:0000}-{1:00}-{2:00}", _year, _month, _day);
			else
				return "LogDate Empty";
		}

		public static bool operator <(LogDate s, LogDate l)
		{
			return (s.Year < l.Year)
				|| ((s.Year == l.Year) && (s.Month < l.Month))
				|| ((s.Year == l.Year) && (s.Month == l.Month) && (s.Day < l.Day));
        }
        public static bool operator >(LogDate l, LogDate s)
        {
            return (s.Year < l.Year)
                || ((s.Year == l.Year) && (s.Month < l.Month))
                || ((s.Year == l.Year) && (s.Month == l.Month) && (s.Day < l.Day));
        }
    }

	public class DateDayZero : Exception { }
    public class DateDayTooLarge : Exception { }
	public class DateMonthZero : Exception { }
    public class DateMonthTooLarge : Exception { }
    public class DateYearTooSmall : Exception { }
}

