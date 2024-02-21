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

    [Fact]
    public void validationRejectsDayTooSmall()
    {
        Assert.Throws<DateDayZero>(() => date.day = 0);
    }

    [Fact]
    public void validationRejectsDayTooLarge()
    {
        Assert.Throws<DateDayTooLarge>(() => date.day = 32);
        Assert.Throws<DateDayTooLarge>(() => date.day = 40);
        Assert.Throws<DateDayTooLarge>(() => date.day = 2000);
    }

    [Fact]
    public void validationRejectsMonthTooSmall()
    {
        Assert.Throws<DateMonthZero>(() => date.month = 0);
    }

    [Fact]
    public void validationRejectsMonthTooLarge()
    {
        Assert.Throws<DateMonthTooLarge>(() => date.month = 13);
        Assert.Throws<DateMonthTooLarge>(() => date.month = 14);
        Assert.Throws<DateMonthTooLarge>(() => date.month = 50);
    }

    [Fact]
    public void validationRejectsYearTooSmall()
    {
        Assert.Throws<DateYearTooSmall>(() => date.year = 0);
        Assert.Throws<DateYearTooSmall>(() => date.year = 1);
        Assert.Throws<DateYearTooSmall>(() => date.year = 1492);
        Assert.Throws<DateYearTooSmall>(() => date.year = 1969);
    }

    [Fact]
    public void validationAllowsYearAfter1970()
    {
        Exception exception = Record.Exception(() => date.year = 1970);
        Assert.Null(exception);
        exception = Record.Exception(() => date.year = 2023);
        Assert.Null(exception);
    }

    [Fact]
    public void validationAppliesToArgs()
    {
        Assert.Throws<DateDayZero>(() =>
        {
            LogDate d = new LogDate(0, 1, 2023);
        });
        Assert.Throws<DateDayTooLarge>(() =>
        {
            LogDate d = new LogDate(40, 1, 2023);
        });
        Assert.Throws<DateMonthZero>(() =>
        {
            LogDate d = new LogDate(1, 0, 2023);
        });
        Assert.Throws<DateMonthTooLarge>(() =>
        {
            LogDate d = new LogDate(1, 13, 2023);
        });
        Assert.Throws<DateYearTooSmall>(() =>
        {
            LogDate d = new LogDate(1, 1, 1969);
        });
    }
}
