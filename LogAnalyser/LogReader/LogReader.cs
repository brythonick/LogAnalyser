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

		public static LogDate ParseDate(string logLine)
		{
			throw new Exception("Not yet implemented");
		}
	}
}