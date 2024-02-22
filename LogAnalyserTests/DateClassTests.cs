using LogAnalyser;

namespace LogAnalyserTests;

public class DateClassTests
{
    private LogDate date = new();

    [Fact]
    public void HoldsDay()
    {
        Exception exception = Record.Exception(() => { date.Day = 1; });
        Assert.Null(exception);
    }

    [Fact]
    public void HoldsMonth()
    {
        Exception exception = Record.Exception(() => { date.Month = 1; });
        Assert.Null(exception);
    }

    [Fact]
    public void HoldsYear()
    {
        Exception exception = Record.Exception(() => { date.Year = 2023; });
        Assert.Null(exception);
    }

    [Fact]
    public void TakesDatePartsAtInit()
    {
        Exception exception = Record.Exception(() => {
            LogDate d = new(1, 1, 2023);
        });
        Assert.Null(exception);
    }

    [Fact]
    public void ValidationRejectsDayTooSmall()
    {
        Assert.Throws<DateDayZero>(() => date.Day = 0);
    }

    [Fact]
    public void ValidationRejectsDayTooLarge()
    {
        Assert.Throws<DateDayTooLarge>(() => date.Day = 32);
        Assert.Throws<DateDayTooLarge>(() => date.Day = 40);
        Assert.Throws<DateDayTooLarge>(() => date.Day = 2000);
    }

    [Fact]
    public void ValidationRejectsMonthTooSmall()
    {
        Assert.Throws<DateMonthZero>(() => date.Month = 0);
    }

    [Fact]
    public void ValidationRejectsMonthTooLarge()
    {
        Assert.Throws<DateMonthTooLarge>(() => date.Month = 13);
        Assert.Throws<DateMonthTooLarge>(() => date.Month = 14);
        Assert.Throws<DateMonthTooLarge>(() => date.Month = 50);
    }

    [Fact]
    public void ValidationRejectsYearTooSmall()
    {
        Assert.Throws<DateYearTooSmall>(() => date.Year = 0);
        Assert.Throws<DateYearTooSmall>(() => date.Year = 1);
        Assert.Throws<DateYearTooSmall>(() => date.Year = 1492);
        Assert.Throws<DateYearTooSmall>(() => date.Year = 1969);
    }

    [Fact]
    public void ValidationAllowsYearAfter1970()
    {
        Exception exception = Record.Exception(() => date.Year = 1970);
        Assert.Null(exception);
        exception = Record.Exception(() => date.Year = 2023);
        Assert.Null(exception);
    }

    [Fact]
    public void ValidationAppliesToArgs()
    {
        Assert.Throws<DateDayZero>(() =>
        {
            LogDate d = new(0, 1, 2023);
        });
        Assert.Throws<DateDayTooLarge>(() =>
        {
            LogDate d = new(40, 1, 2023);
        });
        Assert.Throws<DateMonthZero>(() =>
        {
            LogDate d = new(1, 0, 2023);
        });
        Assert.Throws<DateMonthTooLarge>(() =>
        {
            LogDate d = new(1, 13, 2023);
        });
        Assert.Throws<DateYearTooSmall>(() =>
        {
            LogDate d = new(1, 1, 1969);
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
    public void ComparesLessThan(
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
    public void ComparesMoreThan(
        uint sDay, uint sMonth, uint sYear,
        uint lDay, uint lMonth, uint lYear,
        bool expected)
    {
        LogDate smaller = new(sDay, sMonth, sYear);
        LogDate larger = new(lDay, lMonth, lYear);
        Assert.Equal(expected, larger > smaller);
    }
}
