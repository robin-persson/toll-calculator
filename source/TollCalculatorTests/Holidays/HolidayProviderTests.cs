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
        ThenResultIs(
            new List<DateTime>
            {
                new DateTime(2024, 1, 1),
                new DateTime(2024, 1, 6),
                new DateTime(2024, 12, 31),
            }
        );

        void GivenHolidayProvider()
        {
            holidayProvider = new HolidayProvider();
        }

        void WhenGettingHolidaysFor(int year)
        {
            result = holidayProvider!.GetHolidays(year);
        }

        void ThenResultIs(IEnumerable<DateTime> expectedResult)
        {
            result!.Should().ContainInOrder(expectedResult);
        }
    }
}
