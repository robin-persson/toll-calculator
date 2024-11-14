namespace TollFeeCalculator;

public static class DateTimeExtensions
{
    public static bool IsWithinAnHourOf(this DateTime first, DateTime second)
    {
        return first > second && (first - second) < TimeSpan.FromHours(1);
    }

    public static IEnumerable<IEnumerable<DateTime>> SplitByHour(this IEnumerable<DateTime> input)
    {
        while (input.Any())
        {
            var next = input.First();
            var withinAnHour = from time in input where time.IsWithinAnHourOf(next) select time;
            IEnumerable<DateTime> nextHour = [next, .. withinAnHour];
            yield return nextHour;
            input = input.Except(nextHour);
        }
    }
}
