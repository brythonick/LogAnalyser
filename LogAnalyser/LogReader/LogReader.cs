using System.Text.Json;

namespace LogAnalyser
{
    public class LogReader
	{
		private string _specificationName;
		private LogSpec _spec;

		public LogReader(string specificationName)
		{
			_specificationName = specificationName;
            using FileStream stream = File.OpenRead("./Config/log_specification.json");
            LogSpecifications? _specs = JsonSerializer.Deserialize<LogSpecifications>(stream);
			if (_specs == null)
				throw new Exception("Could not read log specifications.");
			else
			{
				_spec = _specs.GetSpec(_specificationName);
			}

        }

		public string SpecName { get { return _specificationName; } }
		public LogSpec Spec { get { return _spec; } }

		public LogDate ParseDate(string logLine)
		{
			if (_spec.delimiter != null)
			{
                string[] splitLine = logLine.Split(_spec.delimiter);
				logLine = splitLine[_spec.indicies.date];
            }
			LogDate date = new();
			int yearLength = _spec.indicies.yearEnd - _spec.indicies.yearStart;
			date.Year = uint.Parse(logLine.Substring(_spec.indicies.yearStart, yearLength + 1));
            int monthLength = _spec.indicies.monthEnd - _spec.indicies.monthStart;
            date.Month = uint.Parse(logLine.Substring(_spec.indicies.monthStart, monthLength + 1));
            int dayLength = _spec.indicies.dayEnd - _spec.indicies.dayStart;
            date.Day = uint.Parse(logLine.Substring(_spec.indicies.dayStart, dayLength + 1));
			return date;
        }
	}
}