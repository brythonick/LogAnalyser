namespace LogAnalyser
{
    public class LogDate
	{
		public int day { get; set; }
		public int month { get; set; }
        public int year { get; set; }

		public LogDate() { }
        public LogDate(int argDay, int argMonth, int argYear)
		{
			day = argDay; month = argMonth; year = argYear;
		}
	}
}

