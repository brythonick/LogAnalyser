using System.Text.Json;

namespace LogAnalyser
{
	public class LogReader
	{
		private string _specificationName;
		private LogSpec _spec;
		private DateRecorder _dateRecorder = new();

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
			int yearLength = (_spec.indicies.yearEnd - _spec.indicies.yearStart) + 1;
			if (yearLength == 2)
			{
                string year = string.Concat("20", logLine.AsSpan(_spec.indicies.yearStart, yearLength));
                date.Year = uint.Parse(year);
            }
			else
				date.Year = uint.Parse(logLine.Substring(_spec.indicies.yearStart, yearLength));
			int monthLength = (_spec.indicies.monthEnd - _spec.indicies.monthStart) + 1;
			date.Month = uint.Parse(logLine.Substring(_spec.indicies.monthStart, monthLength));
			int dayLength = (_spec.indicies.dayEnd - _spec.indicies.dayStart) + 1;
			date.Day = uint.Parse(logLine.Substring(_spec.indicies.dayStart, dayLength));
			return date;
		}

		public void ParseSource(ILogSource source)
		{
			string? line = source.NextLine();
			while (line != null)
			{
				LogDate date = ParseDate(line);
				_dateRecorder.Add(date);
				line = source.NextLine();
			}
		}

		public LogDate EarliestDate() => _dateRecorder.Dates()[0];
		public LogDate LatestDate() => _dateRecorder.Dates()[^1];
	}
}