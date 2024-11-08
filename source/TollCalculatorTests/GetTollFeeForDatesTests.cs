using FluentAssertions;
using TollFeeCalculator;

namespace TollCalculatorTests;

public class GetTollFeeForDatesTests : TollCalculatorTestsBase
{
    [Fact]
    public void GettingTollFeeForDifferentDates_ThrowsException()
    {
        GivenTollCalculator();
        WhenGettingTollFee_ThenExceptionIsThrown<ExpectedSingleDateException>(
            vehicle: Vehicle.Car,
            timestamps: ["2024-11-08 07:00:00", "2024-11-09 08:00:00"],
            expectedExceptionMessage: "Expected single date but got [2024-11-08 0:00:00, 2024-11-09 0:00:00]"
        );

        void WhenGettingTollFee_ThenExceptionIsThrown<ExceptionType>(
            Vehicle vehicle,
            IEnumerable<string> timestamps,
            string expectedExceptionMessage
        )
            where ExceptionType : Exception
        {
            Action action = () => WhenGettingTollFeeWith(vehicle: vehicle, timestamps: timestamps);
            action.Should().Throw<ExceptionType>().WithMessage(expectedExceptionMessage);
        }
    }

    [Fact]
    public void WhenGettingFeeForSeveralPassesWithinAnHour_OnlyTheMaximumFeeIsCounted()
    {
        GivenTollCalculator();
        WhenGettingTollFeeWith(
            vehicle: Vehicle.Car,
            timestamps: ["2024-11-08 07:30:00", "2024-11-08 07:59:00", "2024-11-08 08:29:00"]
        );
        ThenResultIs(18);
    }

    [Fact]
    public void GettingTollFeeAcrossARangeOfHours_IsCalculatedCorrectly()
    {
        GivenTollCalculator();
        WhenGettingTollFeeWith(
            vehicle: Vehicle.Car,
            timestamps:
            [
                "2024-11-08 07:55:00",
                "2024-11-08 08:05:00",
                "2024-11-08 08:30:00", // 18
                "2024-11-08 14:30:00",
                "2024-11-08 15:25:00", // + 13
                "2024-11-08 15:30:00", // + 18
            ]
        );
        ThenResultIs(18 + 13 + 18);
    }

    [Fact]
    public void Toll_DoesNotExceedDailyMaximum()
    {
        GivenTollCalculator();
        WhenGettingTollFeeWith(
            vehicle: Vehicle.Car,
            timestamps:
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

    private void WhenGettingTollFeeWith(Vehicle vehicle, IEnumerable<string> timestamps)
    {
        var dateTimes = from timestamp in timestamps select DateTime.Parse(timestamp);
        result = calculator!.GetTollFee(vehicle, dateTimes);
    }
}
