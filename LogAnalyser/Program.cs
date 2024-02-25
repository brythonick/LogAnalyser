using LogAnalyser;

try
{
    string module;
    string specification;
    string path;
    module = args[0];
    specification = args[1];
    path = args[2];
    if (module != "dates")
    {
        Console.WriteLine("Error: Other modules are not yet implemented.");
        Environment.Exit(2);
    }
    else
    {
        FileLogSource source = new(path);
        LogReader reader = new(specification);
        reader.ParseSource(source);
        Console.WriteLine(reader.EarliestDate());
        Console.WriteLine(reader.LatestDate());
    }
}
catch (IndexOutOfRangeException)
{
    Console.WriteLine("Error: One or more args missing.");
    Environment.Exit(1);
}