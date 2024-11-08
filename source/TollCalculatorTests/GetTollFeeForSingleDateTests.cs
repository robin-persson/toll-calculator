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
    public void GetTollFee_ReturnsExpectedFee_ForTimeOfDay(string dateString, int expectedFee)
    {
        GivenTollCalculator();
        WhenGettingTollFeeWith(Vehicle.Car, dateString);
        ThenResultIs(expectedFee);
    }

    [Theory]
    [InlineData(Vehicle.Motorbike)]
    [InlineData(Vehicle.Tractor)]
    [InlineData(Vehicle.Emergency)]
    [InlineData(Vehicle.Diplomat)]
    [InlineData(Vehicle.Foreign)]
    [InlineData(Vehicle.Military)]
    public void Toll_IsFree_ForExemptVehicles(Vehicle vehicle)
    {
        GivenTollCalculator();
        WhenGettingTollFeeWith(vehicle, "2024-01-01 07:00:00");
        ThenResultIs(0);
    }

    [Theory]
    [InlineData("2024-02-03 07:00:00")]
    [InlineData("2024-02-04 07:00:00")]
    [InlineData("2024-02-10 07:00:00")]
    [InlineData("2024-02-11 07:00:00")]
    [InlineData("2024-02-17 07:00:00")]
    [InlineData("2024-02-18 07:00:00")]
    [InlineData("2024-02-24 07:00:00")]
    [InlineData("2024-02-25 07:00:00")]
    [InlineData("2024-11-02 07:00:00")]
    [InlineData("2024-11-03 07:00:00")]
    [InlineData("2024-11-09 07:00:00")]
    [InlineData("2024-11-10 07:00:00")]
    [InlineData("2024-11-16 07:00:00")]
    [InlineData("2024-11-17 07:00:00")]
    [InlineData("2024-11-23 07:00:00")]
    [InlineData("2024-11-24 07:00:00")]
    [InlineData("2024-11-30 07:00:00")]
    [InlineData("2024-12-01 07:00:00")]
    public void Toll_IsFree_ForWeekends(string dateString)
    {
        GivenTollCalculator();
        WhenGettingTollFeeWith(Vehicle.Car, dateString);
        ThenResultIs(0);
    }

    [Theory]
    [InlineData("2024-07-01 07:00:00")]
    [InlineData("2024-07-02 07:00:00")]
    [InlineData("2024-07-03 07:00:00")]
    [InlineData("2024-07-04 07:00:00")]
    [InlineData("2024-07-05 07:00:00")]
    [InlineData("2024-07-06 07:00:00")]
    [InlineData("2024-07-07 07:00:00")]
    [InlineData("2024-07-08 07:00:00")]
    [InlineData("2024-07-09 07:00:00")]
    [InlineData("2024-07-10 07:00:00")]
    [InlineData("2024-07-11 07:00:00")]
    [InlineData("2024-07-12 07:00:00")]
    [InlineData("2024-07-13 07:00:00")]
    [InlineData("2024-07-14 07:00:00")]
    [InlineData("2024-07-15 07:00:00")]
    [InlineData("2024-07-16 07:00:00")]
    [InlineData("2024-07-17 07:00:00")]
    [InlineData("2024-07-18 07:00:00")]
    [InlineData("2024-07-19 07:00:00")]
    [InlineData("2024-07-20 07:00:00")]
    [InlineData("2024-07-21 07:00:00")]
    [InlineData("2024-07-22 07:00:00")]
    [InlineData("2024-07-23 07:00:00")]
    [InlineData("2024-07-24 07:00:00")]
    [InlineData("2024-07-25 07:00:00")]
    [InlineData("2024-07-26 07:00:00")]
    [InlineData("2024-07-27 07:00:00")]
    [InlineData("2024-07-28 07:00:00")]
    [InlineData("2024-07-29 07:00:00")]
    [InlineData("2024-07-30 07:00:00")]
    [InlineData("2024-07-31 07:00:00")]
    [InlineData("2025-07-31 07:00:00")]
    [InlineData("2026-07-31 07:00:00")]
    [InlineData("2027-07-31 07:00:00")]
    [InlineData("2028-07-31 07:00:00")]
    public void Toll_IsFree_ForJuly(string dateString)
    {
        GivenTollCalculator();
        WhenGettingTollFeeWith(Vehicle.Car, dateString);
        ThenResultIs(0);
    }

    private void WhenGettingTollFeeWith(Vehicle vehicle, string dateString)
    {
        var date = DateTime.Parse(dateString);

        result = calculator!.GetTollFee(date, vehicle);
    }
}
