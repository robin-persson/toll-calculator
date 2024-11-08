using TollFeeCalculator;

namespace TollCalculatorTests;

public class GetTollFeeForDatesTests : TollCalculatorTestsBase
{
    [Fact]
    public void Toll_DoesNotExceedDailyMaximum()
    {
        GivenTollCalculator();
        WhenGettingTollFeeWith(
            vehicle: Vehicle.Car,
            dates:
            [
                "2024-11-08 07:00:00",
                "2024-11-08 08:00:00",
                "2024-11-08 09:00:00",
                "2024-11-08 10:00:00",
                "2024-11-08 11:00:00",
                "2024-11-08 12:00:00",
                "2024-11-08 13:00:00",
            ]
        );
        ThenResultIs(60);
    }

    private void WhenGettingTollFeeWith(Vehicle vehicle, string[] dates)
    {
        var dateTimes = from date in dates select DateTime.Parse(date);
        result = calculator!.GetTollFee(vehicle, dateTimes);
    }
}
