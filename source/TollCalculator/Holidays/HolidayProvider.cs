namespace TollFeeCalculator.Holidays
{
    public class HolidayProvider : IHolidayProvider
    {
        private static IDictionary<int, DateTime> easterDays = new Dictionary<int, DateTime>
        {
            { 2024, new DateTime(2024, 3, 31) },
        };

        public IEnumerable<DateTime> GetHolidays(int year)
        {
            if (!IsYearSupported())
            {
                throw new UnexpectedYearException(
                    $"Expected one of the supported years [{string.Join(',', easterDays.Keys)}], but got {year}"
                );
            }
            yield return new DateTime(year, 1, 1);
            yield return new DateTime(year, 1, 6);

            var easterDay = easterDays[year];
            yield return easterDay.AddDays(-2);
            yield return easterDay;
            yield return easterDay.AddDays(1);

            yield return new DateTime(year, 5, 1);

            yield return easterDay.AddDays(39);

            yield return new DateTime(year, 12, 25);
            yield return new DateTime(year, 12, 26);

            yield return new DateTime(year, 12, 31);

            bool IsYearSupported()
            {
                return easterDays.ContainsKey(year);
            }
        }
    }
}
