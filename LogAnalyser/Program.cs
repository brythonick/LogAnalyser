using LogAnalyser;

FileLogSource source = new("./Logs/ceredigion.txt");
LogReader reader = new("ceredigion");
reader.ParseSource(source);
Console.WriteLine(reader.EarliestDate());
Console.WriteLine(reader.LatestDate());