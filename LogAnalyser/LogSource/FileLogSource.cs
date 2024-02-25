namespace LogAnalyser
{
    public class FileLogSource : ILogSource
	{
		private readonly string[] _log;
		private Queue<string> _logQueue = new();
		
		public FileLogSource(string path)
		{
			Path = path;
			_log = File.ReadAllLines(path);
			foreach (string line in _log)
			{
				_logQueue.Enqueue(line);
			}
        }

        public string Path { get; set; }

        public string? NextLine()
		{
			try
			{
                return _logQueue.Dequeue();
            }
			catch (InvalidOperationException)
			{
				return null;
			}
			
		}
	}
}

