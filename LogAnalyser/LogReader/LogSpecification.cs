namespace LogAnalyser
{
    public class LogSpecifications
	{
		public required LogSpec ceredigion { get; set; }

		public LogSpec GetSpec(string name)
		{
			if (name == "ceredigion")
				return ceredigion;
			else
				throw new Exception("Given spec does not exist.");
		}
	}

	public class LogSpec
	{
		public required LogIndex indicies { get; set; }
		public string? delimiter { get; set; }
	}

	public class LogIndex
	{
		public int date { get; set; }
		public int yearStart { get; set; }
		public int yearEnd { get; set; }
		public int monthStart { get; set; }
		public int monthEnd { get; set; }
		public int dayStart { get; set; }
		public int dayEnd { get; set; }
	}
}

