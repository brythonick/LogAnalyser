using LogAnalyser;

namespace LogAnalyserTests;

public class DateClassTests
{
    private LogDate date = new LogDate();

    [Fact]
    public void holdsDay()
    {
        Exception exception = Record.Exception(() => { date.day = 1; });
        Assert.Null(exception);
    }

    [Fact]
    public void holdsMonth()
    {
        Exception exception = Record.Exception(() => { date.month = 1; });
        Assert.Null(exception);
    }

    [Fact]
    public void holdsYear()
    {
        Exception exception = Record.Exception(() => { date.year = 2023; });
        Assert.Null(exception);
    }

    [Fact]
    public void takesDatePartsAtInit()
    {
        Exception exception = Record.Exception(() => {
            LogDate d = new LogDate(1, 1, 2023);
        });
        Assert.Null(exception);
    }
}
