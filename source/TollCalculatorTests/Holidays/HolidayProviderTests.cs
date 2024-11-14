using FluentAssertions;
using TollFeeCalculator.Holidays;

namespace TollCalculatorTests.Holidays;

public class HolidayProviderTests
{
    private IHolidayProvider? holidayProvider;
    private IEnumerable<DateTime>? result;

    [Fact]
    public void GetHolidays_For2024_ReturnsNewYearHolidays()
    {
        GivenHolidayProvider();
        WhenGettingHolidaysFor(2024);
        ThenResultIs(new List<DateTime> { new DateTime(2024, 1, 1), new DateTime(2024, 12, 31) });
    }

    [Fact]
    public void GetHolidays_For2024_ReturnsChristmasHolidays()
    {
        GivenHolidayProvider();
        WhenGettingHolidaysFor(2024);
        ThenResultIs(
            new List<DateTime>
            {
                new DateTime(2024, 1, 6),
                new DateTime(2024, 12, 25),
                new DateTime(2024, 12, 26),
            }
        );
    }

    [Fact]
    public void GetHolidays_For2024_ReturnsEasterHolidays()
    {
        GivenHolidayProvider();
        WhenGettingHolidaysFor(2024);
        ThenResultIs(
            new List<DateTime>
            {
                new DateTime(2024, 3, 29),
                new DateTime(2024, 3, 31),
                new DateTime(2024, 4, 1),
            }
        );
    }

    [Fact]
    public void GetHolidays_ForUnsupportedYear_ThrowsException()
    {
        GivenHolidayProvider();
        WhenGettingHolidays_ThenExceptionIsThrown<UnexpectedYearException>(
            year: 2025,
            expectedMessage: "Expected one of the supported years [2024], but got 2025"
        );

        void WhenGettingHolidays_ThenExceptionIsThrown<TException>(int year, string expectedMessage)
            where TException : Exception
        {
            holidayProvider!
                .Invoking(provider => provider.GetHolidays(year).ToList())
                .Should()
                .Throw<TException>()
                .WithMessage(expectedMessage);
        }
    }

    private void GivenHolidayProvider()
    {
        holidayProvider = new HolidayProvider();
    }

    private void WhenGettingHolidaysFor(int year)
    {
        result = holidayProvider!.GetHolidays(year);
    }

    private void ThenResultIs(IEnumerable<DateTime> expectedResult)
    {
        result!.Should().ContainInOrder(expectedResult);
    }
}
