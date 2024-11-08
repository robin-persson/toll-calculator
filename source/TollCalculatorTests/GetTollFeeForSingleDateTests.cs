using TollFeeCalculator;

namespace TollCalculatorTests;

public class GetTollFeeForSingleDateTests : TollCalculatorTestsBase
{
    [Theory]
    [InlineData("2024-01-01 00:00:00", 0)]
    [InlineData("2024-01-01 03:59:00", 0)]
    [InlineData("2024-01-01 05:00:00", 0)]
    [InlineData("2024-01-01 06:29:00", 8)]
    [InlineData("2024-01-01 06:30:00", 13)]
    [InlineData("2024-01-01 06:59:00", 13)]
    [InlineData("2024-01-01 07:00:00", 18)]
    [InlineData("2024-01-01 08:00:00", 13)]
    [InlineData("2024-01-01 08:30:00", 8)]
    [InlineData("2024-01-01 09:00:00", 8)]
    [InlineData("2024-01-01 09:30:00", 8)]
    [InlineData("2024-01-01 11:15:00", 8)]
    [InlineData("2024-01-01 12:45:00", 8)]
    [InlineData("2024-01-01 15:00:00", 13)]
    [InlineData("2024-01-01 15:30:00", 18)]
    [InlineData("2024-01-01 16:00:00", 18)]
    [InlineData("2024-01-01 16:30:00", 18)]
    [InlineData("2024-01-01 17:00:00", 13)]
    [InlineData("2024-01-01 17:30:00", 13)]
    [InlineData("2024-01-01 18:00:00", 8)]
    [InlineData("2024-01-01 18:30:00", 0)]
    [InlineData("2024-01-01 21:03:00", 0)]
    [InlineData("2024-01-01 23:57:00", 0)]
    public void GetTollFee_ReturnsExpected_ForTimeOfDay(string dateString, int expectedFee)
    {
        var date = DateTime.Parse(dateString);

        WhenGettingTollFeeWith(new Car(), date);
        ThenResultIs(expectedFee);
    }

    private void WhenGettingTollFeeWith(Vehicle vehicle, DateTime date)
    {
        var tollFeeCalculator = new TollCalculator();
        result = tollFeeCalculator.GetTollFee(date, vehicle);
    }
}
