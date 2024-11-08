using FluentAssertions;
using TollFeeCalculator;

namespace TollCalculatorTests;

public class GetTollFeeForDatesTests
{
    private int? result;

    [Fact]
    public void GetTollFee_IsCallable()
    {
        WhenGettingTollFeeWith(vehicle: new Car(), dates: [new DateTime(2021, 1, 1, 9, 0, 0)]);
        ThenItReturnsAResult();

        void WhenGettingTollFeeWith(Vehicle vehicle, DateTime[] dates)
        {
            var tollFeeCalculator = new TollCalculator();
            result = tollFeeCalculator.GetTollFee(vehicle, dates);
        }

        void ThenItReturnsAResult()
        {
            result.Should().NotBe(null);
        }
    }
}
