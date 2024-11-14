namespace TollFeeCalculator.Holidays
{
    public class HolidayProvider : IHolidayProvider
    {
        private static IDictionary<int, IEnumerable<DateTime>> easterHolidays = new Dictionary<
            int,
            IEnumerable<DateTime>
        >
        {
            {
                2024,
                new List<DateTime>
                {
                    new DateTime(2024, 3, 29),
                    new DateTime(2024, 3, 31),
                    new DateTime(2024, 4, 1),
                }
            },
        };

        public IEnumerable<DateTime> GetHolidays(int year)
        {
            if (!IsYearSupported())
            {
                throw new UnexpectedYearException(
                    $"Expected one of the supported years [{string.Join(',', easterHolidays.Keys)}], but got {year}"
                );
            }
            yield return new DateTime(year, 1, 1);
            yield return new DateTime(year, 1, 6);

            foreach (var holiday in EasterHolidays())
            {
                yield return holiday;
            }

            yield return new DateTime(year, 5, 1);

            yield return new DateTime(year, 12, 25);
            yield return new DateTime(year, 12, 26);

            yield return new DateTime(year, 12, 31);

            bool IsYearSupported()
            {
                return easterHolidays.ContainsKey(year);
            }

            IEnumerable<DateTime> EasterHolidays()
            {
                return easterHolidays[year];
            }
        }
    }
}
