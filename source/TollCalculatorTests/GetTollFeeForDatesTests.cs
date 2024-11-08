using TollFeeCalculator;

namespace TollCalculatorTests;

public class GetTollFeeForDatesTests : TollCalculatorTestsBase
{
    [Fact]
    public void GetTollFee_IsCallable()
    {
        GivenTollCalculator();
        WhenGettingTollFeeWith(vehicle: Vehicle.Car, dates: [new DateTime(2021, 1, 1, 9, 0, 0)]);
        ThenItReturnsAResult();

        void WhenGettingTollFeeWith(Vehicle vehicle, DateTime[] dates)
        {
            result = calculator!.GetTollFee(vehicle, dates);
        }
    }
}
