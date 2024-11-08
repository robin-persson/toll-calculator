using TollFeeCalculator;

namespace TollCalculatorTests;

public class GetTollFeeForSingleDateTests : TollCalculatorTestsBase
{
    [Fact]
    public void GetTollFee_IsCallable()
    {
        WhenGettingTollFeeWith(vehicle: new Car(), date: new DateTime(2021, 1, 1, 9, 0, 0));
        ThenItReturnsAResult();

        void WhenGettingTollFeeWith(Vehicle vehicle, DateTime date)
        {
            var tollFeeCalculator = new TollCalculator();
            result = tollFeeCalculator.GetTollFee(date, vehicle);
        }
    }
}
