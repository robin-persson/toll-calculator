using FluentAssertions;
using Moq;
using Nager.Date.HolidayProviders;
using Nager.Date.Models;
using TollFeeCalculator;

namespace TollCalculatorTests;

public class TollCalculatorTestsBase
{
    protected int? result;
    protected TollCalculator? calculator;
    private Mock<IHolidayProvider> holidayProviderMock = new Mock<IHolidayProvider>();

    public void Initialize()
    {
        holidayProviderMock
            .Setup(provider => provider.GetHolidays(It.IsAny<int>()))
            .Returns(new List<Holiday>());
    }

    protected void GivenTollCalculator()
    {
        calculator = new TollCalculator(holidayProviderMock.Object);
    }

    protected void ThenItReturnsAResult()
    {
        result.Should().NotBe(null);
    }

    protected void ThenResultIs(int expectedResult)
    {
        result.Should().Be(expectedResult);
    }

    public void GivenHolidays(IEnumerable<string> dates)
    {
        holidayProviderMock
            .Setup(provider => provider.GetHolidays(It.IsAny<int>()))
            .Returns(holidays());

        IEnumerable<Holiday> holidays()
        {
            foreach (string date in dates)
            {
                yield return new Holiday { Date = DateTime.Parse(date) };
            }
        }
    }
}
