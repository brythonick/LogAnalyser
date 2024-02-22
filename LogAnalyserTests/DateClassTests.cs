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

    [Theory]
    [InlineData((uint)1, (uint)1, (uint)1970, (uint)1, (uint)1, (uint)2023, true)]
    [InlineData((uint)1, (uint)1, (uint)1970, (uint)1, (uint)1, (uint)1970, false)]
    [InlineData((uint)1, (uint)1, (uint)2023, (uint)1, (uint)2, (uint)2023, true)]
    [InlineData((uint)1, (uint)1, (uint)2023, (uint)2, (uint)1, (uint)2023, true)]
    [InlineData((uint)1, (uint)1, (uint)2023, (uint)1, (uint)1, (uint)2022, false)]
    [InlineData((uint)5, (uint)10, (uint)2023, (uint)20, (uint)11, (uint)2023, true)]
    [InlineData((uint)4, (uint)4, (uint)2003, (uint)4, (uint)4, (uint)2023, true)]
    [InlineData((uint)18, (uint)3, (uint)2020, (uint)31, (uint)9, (uint)2019, false)]
    [InlineData((uint)31, (uint)12, (uint)1999, (uint)30, (uint)12, (uint)1999, false)]
    [InlineData((uint)31, (uint)12, (uint)1999, (uint)1, (uint)1, (uint)2000, true)]
    public void comparesLessThan(
        uint sDay, uint sMonth, uint sYear,
        uint lDay, uint lMonth, uint lYear,
        bool expected)
    {
        LogDate smaller = new(sDay, sMonth, sYear);
        LogDate larger = new(lDay, lMonth, lYear);
        Assert.Equal(expected, smaller < larger);
    }

    [Theory]
    [InlineData((uint)1, (uint)1, (uint)1970, (uint)1, (uint)1, (uint)2023, true)]
    [InlineData((uint)1, (uint)1, (uint)1970, (uint)1, (uint)1, (uint)1970, false)]
    [InlineData((uint)1, (uint)1, (uint)2023, (uint)1, (uint)2, (uint)2023, true)]
    [InlineData((uint)1, (uint)1, (uint)2023, (uint)2, (uint)1, (uint)2023, true)]
    [InlineData((uint)1, (uint)1, (uint)2024, (uint)1, (uint)1, (uint)2020, false)]
    [InlineData((uint)5, (uint)10, (uint)2023, (uint)20, (uint)11, (uint)2023, true)]
    [InlineData((uint)24, (uint)4, (uint)2003, (uint)4, (uint)5, (uint)2003, true)]
    [InlineData((uint)18, (uint)3, (uint)2020, (uint)31, (uint)9, (uint)1987, false)]
    [InlineData((uint)31, (uint)12, (uint)1999, (uint)30, (uint)12, (uint)1999, false)]
    [InlineData((uint)31, (uint)12, (uint)1999, (uint)1, (uint)1, (uint)2000, true)]
    public void comparesMoreThan(
        uint sDay, uint sMonth, uint sYear,
        uint lDay, uint lMonth, uint lYear,
        bool expected)
    {
        LogDate smaller = new(sDay, sMonth, sYear);
        LogDate larger = new(lDay, lMonth, lYear);
        Assert.Equal(expected, larger > smaller);
    }
}
