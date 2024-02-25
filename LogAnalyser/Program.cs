using LogAnalyser;

FileLogSource source = new("./Logs/ceredigion.txt");
string? s1 = source.NextLine();
string? s2 = source.NextLine();
string? s3 = source.NextLine();
string? s4 = source.NextLine();