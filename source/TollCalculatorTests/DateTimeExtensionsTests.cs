using FluentAssertions;
using TollFeeCalculator;

namespace TollCalculatorTests;

public class DateTimeExtensionsTests
{
    private IEnumerable<DateTime>? timestamps;
    private IEnumerable<IEnumerable<DateTime>>? result;

    [Fact]
    public void SplitByHour_ShouldSplitInputByHour()
    {
        GivenListOfTimestamps(
            new List<DateTime>()
            {
                new DateTime(2022, 1, 1, 8, 0, 0),
                new DateTime(2022, 1, 1, 8, 15, 0),
                new DateTime(2022, 1, 1, 9, 0, 0),
                new DateTime(2022, 1, 1, 9, 30, 0),
                new DateTime(2022, 1, 1, 10, 0, 0),
                new DateTime(2022, 1, 1, 11, 0, 0),
                new DateTime(2022, 1, 1, 12, 0, 0),
            }
        );
        WhenSplittingByHour();
        ThenResultIs(
            new List<List<DateTime>>
            {
                new List<DateTime>
                {
                    new DateTime(2022, 1, 1, 8, 0, 0),
                    new DateTime(2022, 1, 1, 8, 15, 0),
                },
                new List<DateTime>
                {
                    new DateTime(2022, 1, 1, 9, 0, 0),
                    new DateTime(2022, 1, 1, 9, 30, 0),
                },
                new List<DateTime> { new DateTime(2022, 1, 1, 10, 0, 0) },
                new List<DateTime> { new DateTime(2022, 1, 1, 11, 0, 0) },
                new List<DateTime> { new DateTime(2022, 1, 1, 12, 0, 0) },
            }
        );

        void GivenListOfTimestamps(IEnumerable<DateTime> timestamps)
        {
            this.timestamps = timestamps;
        }

        void WhenSplittingByHour()
        {
            result = timestamps!.SplitByHour();
        }

        void ThenResultIs(IEnumerable<IEnumerable<DateTime>> expectedResult)
        {
            Listify(result!).Should().BeEquivalentTo(Listify(expectedResult));

            IEnumerable<List<DateTime>> Listify(IEnumerable<IEnumerable<DateTime>> input)
            {
                foreach (var item in input)
                {
                    yield return item.ToList();
                }
            }
        }
    }
}
