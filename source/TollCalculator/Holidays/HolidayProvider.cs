namespace TollFeeCalculator.Holidays
{
    public class HolidayProvider : IHolidayProvider
    {
        public IEnumerable<DateTime> GetHolidays(int year)
        {
            yield return new DateTime(year, 1, 1);
            yield return new DateTime(year, 1, 6);
            yield return new DateTime(year, 12, 31);
        }
    }
}
